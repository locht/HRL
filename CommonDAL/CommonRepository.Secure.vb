Imports Framework.Data
Imports System.Data.Objects
Imports System.Transactions
Imports System.Data.Entity
Imports System.Data.Common
Imports System.Text
Imports Framework.Data.System.Linq.Dynamic
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net
Imports System.IO
Imports Framework.Data.SystemConfig
Imports System.Configuration
Imports Framework.Data.DataAccess
Imports Oracle.DataAccess.Client
Imports System.Reflection

Partial Public Class CommonRepository

#Region "Case config"
    Public Function GetCaseConfigByID(ByVal codename As String, ByVal codecase As String) As Integer
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_CODE_NAME = codename,
                                    .P_CODE_CASE = codecase,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_COMMON_LIST.GET_SE_CASE_CONFIG", obj)
                Return Integer.Parse(obj.P_OUT)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Controls Manage"

    ''' <summary>
    ''' Hiển thị d/sach Tên page điều chỉnh control
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="Sorts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_FunctionWithControl_List(ByVal _filter As FunctionDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "NAME ASC") As List(Of FunctionDTO)

        Dim query = (From p In Context.SE_FUNCTION
                     From func In Context.SE_VIEW_CONFIG.Where(Function(f) f.CODE_NAME.ToUpper = p.FID.ToUpper)
                    Select New FunctionDTO With {
                        .ID = p.ID,
                        .NAME = p.NAME,
                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Không áp dụng"),
                        .FID = p.FID,
                        .FUNCTION_GROUP_ID = p.GROUP_ID
                    })
        If _filter.FID <> "" Then
            query = query.Where(Function(p) p.FID.ToUpper.Contains(_filter.FID.ToUpper))
        End If
        If _filter.NAME <> "" Then
            query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
        End If
        If _filter.FUNCTION_GROUP_ID <> 0 Then
            query = query.Where(Function(p) p.FUNCTION_GROUP_ID = _filter.FUNCTION_GROUP_ID)
        End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
        End If

        If PageSize <> -1 Then
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
        End If
        Dim result = (From p In query Select p)
        Return result.ToList
    End Function

    Public Function Insert_Update_Control_List(ByVal _id As String, ByVal Config As String, ByVal log As UserLog) As Boolean
        Dim objSe_ViewDataNew As New SE_VIEW_CONFIG
        Try
            Dim objSe_ViewData As SE_VIEW_CONFIG = (From p In Context.SE_VIEW_CONFIG Where p.CODE_NAME = _id).FirstOrDefault
            If objSe_ViewData IsNot Nothing Then
                Context.SE_VIEW_CONFIG.DeleteObject(objSe_ViewData)
                objSe_ViewDataNew.CODE_NAME = _id
                objSe_ViewDataNew.CONFIG_DATA = Config
                Context.SE_VIEW_CONFIG.AddObject(objSe_ViewDataNew)
            Else
                objSe_ViewDataNew.CODE_NAME = _id
                objSe_ViewDataNew.CONFIG_DATA = Config
                Context.SE_VIEW_CONFIG.AddObject(objSe_ViewDataNew)
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "User List"

    Public Function GetUser(ByVal _filter As UserDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)

        Dim query = (From p In Context.SE_USER
                     Where (From n In p.SE_GROUPS Where n.IS_ADMIN = True).Count = 0
                     Select New UserDTO With {
                        .ID = p.ID,
                        .EMAIL = p.EMAIL,
                        .PASSWORD = p.PASSWORD,
                        .TELEPHONE = p.TELEPHONE,
                        .USERNAME = p.USERNAME,
                        .FULLNAME = p.FULLNAME,
                        .IS_APP = p.IS_APP,
                        .IS_PORTAL = p.IS_PORTAL,
                        .IS_AD = p.IS_AD,
                        .ACTFLG = p.ACTFLG,
                        .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                        .EFFECT_DATE = p.EFFECT_DATE,
                        .EXPIRE_DATE = p.EXPIRE_DATE,
                        .CREATED_DATE = p.CREATED_DATE})

        If _filter.USERNAME <> "" Then
            query = query.Where(Function(p) p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper))
        End If
        If _filter.FULLNAME <> "" Then
            query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
        End If
        If _filter.EMPLOYEE_CODE <> "" Then
            query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
        End If
        If _filter.IS_APP IsNot Nothing Then
            Dim is_short = If(_filter.IS_APP, 1, 0)
            query = query.Where(Function(p) p.IS_APP = is_short)
        End If
        If _filter.IS_PORTAL IsNot Nothing Then
            Dim is_short = If(_filter.IS_PORTAL, 1, 0)
            query = query.Where(Function(p) p.IS_PORTAL = is_short)
        End If
        If _filter.IS_AD IsNot Nothing Then
            Dim is_short = If(_filter.IS_AD, 1, 0)
            query = query.Where(Function(p) p.IS_AD = is_short)
        End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
        End If
        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Return query.ToList
    End Function
    Public Function GetUserList(ByVal _filter As UserDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)

        Dim query = (From p In Context.SE_USER
                     Where (From n In p.SE_GROUPS Where n.IS_ADMIN = True).Count = 0
                     Select New UserDTO With {
                        .ID = p.ID,
                        .EMAIL = p.EMAIL,
                        .PASSWORD = p.PASSWORD,
                        .TELEPHONE = p.TELEPHONE,
                        .USERNAME = p.USERNAME,
                        .FULLNAME = p.FULLNAME,
                        .IS_APP = p.IS_APP,
                        .IS_PORTAL = p.IS_PORTAL,
                        .IS_AD = p.IS_AD,
                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                        .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                        .EFFECT_DATE = p.EFFECT_DATE,
                        .EXPIRE_DATE = p.EXPIRE_DATE,
                        .CREATED_DATE = p.CREATED_DATE})

        If _filter.USERNAME <> "" Then
            query = query.Where(Function(p) p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper))
        End If
        If _filter.FULLNAME <> "" Then
            query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
        End If
        If _filter.EMPLOYEE_CODE <> "" Then
            query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
        End If
        If _filter.TELEPHONE <> "" Then
            query = query.Where(Function(p) p.TELEPHONE.ToUpper.Contains(_filter.TELEPHONE.ToUpper))
        End If
        If _filter.EMAIL <> "" Then
            query = query.Where(Function(p) p.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper))
        End If
        'If _filter.EFFECT_DATE IsNot Nothing Then
        '    query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
        'End If
        'If _filter.EXPIRE_DATE IsNot Nothing Then
        '    query = query.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
        'End If
        'If _filter.IS_AD = True Then
        '    'Dim is_short = If(_filter.IS_APP, 1, 0)
        '    query = query.Where(Function(p) p.IS_APP = _filter.IS_APP)
        'End If
        'If _filter.IS_PORTAL = True Then
        '    'Dim is_short = If(_filter.IS_PORTAL, 1, 0)
        '    query = query.Where(Function(p) p.IS_PORTAL = _filter.IS_PORTAL)
        'End If
        'If _filter.IS_AD = True Then
        '    'Dim is_short = If(_filter.IS_AD, 1, 0)
        '    query = query.Where(Function(p) p.IS_AD = _filter.IS_AD)
        'End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
        End If
        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Return query.ToList
    End Function

    Public Function ValidateUser(ByVal _validate As UserDTO) As Boolean
        Dim query
        If _validate.USERNAME <> "" Then
            query = (From p In Context.SE_USER Where p.USERNAME.ToUpper = _validate.USERNAME.ToUpper).FirstOrDefault
            Return (query Is Nothing)
        End If
        If _validate.EMAIL <> "" Then
            query = (From p In Context.SE_USER Where p.EMAIL.ToUpper = _validate.EMAIL.ToUpper).FirstOrDefault
            Return (query Is Nothing)
        End If
        If _validate.EMPLOYEE_CODE <> "" Then
            query = (From p In Context.HU_EMPLOYEE
                     Where p.EMPLOYEE_CODE.ToUpper = _validate.EMPLOYEE_CODE.ToUpper
                     Select p.EMPLOYEE_CODE).FirstOrDefault
            Return (query IsNot Nothing)
        End If
        Return True
    End Function

    Public Function InsertUser(ByVal _user As UserDTO, ByVal log As UserLog) As Boolean
        Dim objUserData As New SE_USER
        Try
            objUserData.ID = Utilities.GetNextSequence(Context, Context.SE_USER.EntitySet.Name)
            objUserData.PASSWORD = _user.PASSWORD
            objUserData.EMAIL = _user.EMAIL
            objUserData.TELEPHONE = _user.TELEPHONE
            objUserData.USERNAME = _user.USERNAME
            objUserData.FULLNAME = _user.FULLNAME
            objUserData.IS_APP = _user.IS_APP
            objUserData.IS_PORTAL = _user.IS_PORTAL
            objUserData.IS_AD = _user.IS_AD
            objUserData.ACTFLG = "A"
            objUserData.IS_CHANGE_PASS = "0"
            objUserData.EMPLOYEE_CODE = _user.EMPLOYEE_CODE

            If _user.EFFECT_DATE IsNot Nothing Then
                objUserData.EFFECT_DATE = _user.EFFECT_DATE
            End If
            If _user.EXPIRE_DATE IsNot Nothing Then
                objUserData.EXPIRE_DATE = _user.EXPIRE_DATE
            End If


            Context.SE_USER.AddObject(objUserData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ModifyUser(ByVal _user As UserDTO, ByVal log As UserLog) As Boolean
        Try
            Dim objUserData As SE_USER = (From p In Context.SE_USER Where p.ID = _user.ID).FirstOrDefault
            If objUserData IsNot Nothing Then
                objUserData.PASSWORD = _user.PASSWORD
                objUserData.EMAIL = _user.EMAIL
                objUserData.TELEPHONE = _user.TELEPHONE
                objUserData.USERNAME = _user.USERNAME
                objUserData.FULLNAME = _user.FULLNAME
                objUserData.IS_APP = _user.IS_APP
                objUserData.IS_PORTAL = _user.IS_PORTAL
                objUserData.IS_AD = _user.IS_AD
                objUserData.EMPLOYEE_CODE = _user.EMPLOYEE_CODE
                objUserData.EFFECT_DATE = _user.EFFECT_DATE
                objUserData.EXPIRE_DATE = _user.EXPIRE_DATE
                objUserData.MODIFIED_DATE = DateTime.Now
                objUserData.MODIFIED_BY = log.Username
                objUserData.MODIFIED_LOG = log.ComputerName
                Context.SaveChanges(log)
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteUser(ByVal _lstUserID As List(Of Decimal), ByRef _error As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstIDUser As List(Of SE_USER) = (From p In Context.SE_USER Where _lstUserID.Contains(p.ID) Select p).ToList
            If lstIDUser IsNot Nothing Then
                For index As Integer = 0 To lstIDUser.Count - 1
                    Dim userid As Decimal = lstIDUser(index).ID
                    If lstIDUser(index).SE_GROUPS.Count > 0 Then
                        _error = "MESSAGE_DELETE_DATA_USED"
                        Return False
                    End If
                    If (From p In Context.SE_USER_ORG_ACCESS
                        Where userid = p.USER_ID).Count Then
                        _error = "MESSAGE_DELETE_DATA_USED"
                        Return False
                    End If
                    If (From p In Context.SE_USER_PERMISSION
                        Where userid = p.USER_ID).Count Then
                        _error = "MESSAGE_DELETE_DATA_USED"
                        Return False
                    End If
                    Context.SE_USER.DeleteObject(lstIDUser(index))
                Next
                Context.SaveChanges(log)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function UpdateUserListStatus(ByVal _lstUserID As List(Of Decimal), ByVal _status As String, ByVal log As UserLog) As Boolean
        Try
            'Dim lstIDUser As List(Of SE_USER) = (From p In Context.SE_USER Where _lstUserID.Contains(p.ID) Select p).ToList
            'If lstIDUser IsNot Nothing Then
            For index As Integer = 0 To _lstUserID.Count - 1
                Dim _user = New SE_USER With {.ID = _lstUserID(index)}
                Context.SE_USER.Attach(_user)
                _user.ACTFLG = _status
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function SyncUserList(ByRef _newUser As String,
                                 ByRef _modifyUser As String,
                                 ByRef _deleteUser As String,
                                 ByVal log As UserLog) As Boolean
        Try
            _newUser = ""
            _modifyUser = ""
            _deleteUser = ""
            Dim idTer As Decimal = CommonCommon.OT_WORK_STATUS.TERMINATE_ID
            'Kiểm tra nhân viên mới
            Dim lstUser As List(Of UserDTO) = (From p In Context.SE_USER
                                               Select New UserDTO With {
                                                   .ID = p.ID,
                                                   .USERNAME = p.USERNAME,
                                                   .FULLNAME = p.FULLNAME,
                                                   .TELEPHONE = p.TELEPHONE,
                                                   .EMAIL = p.EMAIL,
                                                   .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                   .MODULE_ADMIN = p.MODULE_ADMIN,
                                                   .ACTFLG = p.ACTFLG}).ToList

            Dim lst As List(Of String) = (From p In lstUser Select p.USERNAME.ToUpper).ToList

            Dim lstNew As List(Of UserDTO) = (From p In Context.HU_EMPLOYEE
                                              From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                                              Where Not lst.Contains(cv.WORK_EMAIL.ToUpper) And
                                              (p.WORK_STATUS <> idTer Or p.WORK_STATUS Is Nothing) And
                                              cv.WORK_EMAIL IsNot Nothing
                                              Select New UserDTO With {
                                                 .EFFECT_DATE = DateTime.Now,
                                                 .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                 .FULLNAME = p.FULLNAME_VN,
                                                 .EMAIL = cv.WORK_EMAIL,
                                                 .TELEPHONE = cv.MOBILE_PHONE,
                                                 .IS_AD = True,
                                                 .IS_APP = False,
                                                 .IS_PORTAL = True,
                                                 .IS_CHANGE_PASS = "-1",
                                                 .ACTFLG = "A",
                                                 .PASSWORD = p.EMPLOYEE_CODE,
                                                 .USERNAME = cv.WORK_EMAIL.ToUpper}).ToList

            If lstNew.Count > 0 Then
                Using EncryptData As New EncryptData
                    For i = 0 To lstNew.Count - 1
                        _newUser &= ", " & (lstNew(i).USERNAME)
                        Dim _new As New SE_USER
                        _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER.EntitySet.Name)
                        _new.EFFECT_DATE = lstNew(i).EFFECT_DATE
                        _new.EMPLOYEE_CODE = lstNew(i).EMPLOYEE_CODE
                        _new.FULLNAME = lstNew(i).FULLNAME
                        _new.EMAIL = lstNew(i).EMAIL
                        _new.TELEPHONE = lstNew(i).TELEPHONE
                        _new.IS_AD = lstNew(i).IS_AD
                        _new.IS_APP = lstNew(i).IS_APP
                        _new.IS_PORTAL = lstNew(i).IS_PORTAL
                        _new.IS_CHANGE_PASS = lstNew(i).IS_CHANGE_PASS
                        _new.ACTFLG = lstNew(i).ACTFLG
                        _new.PASSWORD = EncryptData.EncryptString(lstNew(i).PASSWORD)
                        _new.USERNAME = lstNew(i).USERNAME
                        _new.MODULE_ADMIN = ""
                        Context.SE_USER.AddObject(_new)
                    Next

                End Using
            End If
            If _newUser <> "" Then _newUser = _newUser.Substring(2)

            'Kiểm tra nhân viên có thay đổi thông tin
            Dim lstCompare As List(Of UserDTO)
            lstCompare = (From p In Context.HU_EMPLOYEE
                          From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                          From user In Context.SE_USER.Where(Function(f) f.EMPLOYEE_CODE = p.EMPLOYEE_CODE And
                                                                 f.MODULE_ADMIN Is Nothing)
                          Where (p.FULLNAME_VN <> user.FULLNAME Or _
                          cv.MOBILE_PHONE <> user.TELEPHONE Or _
                          cv.WORK_EMAIL <> user.EMAIL) And cv.WORK_EMAIL IsNot Nothing
                          Select New UserDTO With {
                              .ID = user.ID,
                              .USERNAME = user.USERNAME,
                              .FULLNAME = p.FULLNAME_VN,
                              .TELEPHONE = cv.MOBILE_PHONE,
                              .EMAIL = cv.WORK_EMAIL}).ToList


            For i = 0 To lstCompare.Count - 1
                Dim id = lstCompare(i).ID
                _modifyUser &= ", " & (lstCompare(i).USERNAME)
                Dim query = (From p In Context.SE_USER Where p.ID = id).FirstOrDefault
                query.FULLNAME = lstCompare(i).FULLNAME
                query.TELEPHONE = lstCompare(i).TELEPHONE
                query.EMAIL = lstCompare(i).EMAIL
                query.USERNAME = lstCompare(i).EMAIL.ToUpper
            Next
            If _modifyUser <> "" Then _modifyUser = _modifyUser.Substring(2)

            'Kiểm tra nhân viên bị xóa
            Dim lstUserDelete = (From p In Context.HU_EMPLOYEE
                   From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                   From user In Context.SE_USER.Where(Function(f) f.EMPLOYEE_CODE = p.EMPLOYEE_CODE)
                   Where (p.WORK_STATUS <> idTer Or p.WORK_STATUS Is Nothing Or cv.WORK_EMAIL Is Nothing) And
                      user.ACTFLG = "A" And user.MODULE_ADMIN.Length = 0
                      Select user).ToList

            For i = 0 To lstUserDelete.Count - 1
                _deleteUser &= ", " & (lstUserDelete(i).USERNAME)
                Dim id As Decimal = lstUserDelete(i).ID
                lstUserDelete(i).ACTFLG = "I"
            Next
            If _deleteUser <> "" Then _deleteUser = _deleteUser.Substring(2)

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ResetUserPassword(ByVal _userid As List(Of Decimal),
                                      ByVal _minLength As Integer,
                                      ByVal _hasLowerChar As Boolean,
                                      ByVal _hasUpperChar As Boolean,
                                      ByVal _hasNumbericChar As Boolean,
                                      ByVal _hasSpecialChar As Boolean) As Boolean
        Try
            'Lấy danh sách user
            Dim lst As List(Of SE_USER) = (From p In Context.SE_USER
                                       Where _userid.Contains(p.ID)
                                       Select p).ToList
            'Lấy thông tin config password
            Dim rndPass As New RandomPassword
            rndPass.HAS_LOWER_CHAR = _hasLowerChar
            rndPass.HAS_NUMERIC_CHAR = _hasNumbericChar
            rndPass.HAS_SPECIAL_CHAR = _hasSpecialChar
            rndPass.HAS_UPPER_CHAR = _hasUpperChar
            For i = 0 To lst.Count - 1
                Using EncryptData As New EncryptData
                    lst(i).PASSWORD = EncryptData.EncryptString(rndPass.Generate(_minLength))
                    lst(i).IS_CHANGE_PASS = 1

                End Using
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Group List"
    Public Function GetGroupListToComboListBox() As List(Of GroupDTO)
        Dim query = (From p In Context.SE_GROUP.ToList
                     Where CBool(p.IS_ADMIN) = False And p.ACTFLG.ToUpper = "A"
                     Order By p.NAME Select New GroupDTO With {
                     .ID = p.ID,
                     .NAME = p.NAME,
                     .CODE = p.CODE})
        Return query.ToList
    End Function
    Public Function GetGroupList(ByVal _filter As GroupDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "MODIFIED_DATE desc") As List(Of GroupDTO)

        Dim query = (From p In Context.SE_GROUP
                     Where CBool(p.IS_ADMIN) = False
                      Order By p.NAME Select New GroupDTO With {
                     .ID = p.ID,
                     .NAME = p.NAME,
                     .CODE = p.CODE,
                     .EFFECT_DATE = p.EFFECT_DATE,
                     .EXPIRE_DATE = p.EXPIRE_DATE,
                     .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                     .MODIFIED_DATE = p.MODIFIED_DATE})

        If _filter.CODE <> "" Then
            query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
        End If
        If _filter.NAME <> "" Then
            query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
        End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
        End If
        If _filter.EFFECT_DATE IsNot Nothing Then
            query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
        End If
        If _filter.EXPIRE_DATE IsNot Nothing Then
            query = query.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
        End If
        query = query.AsQueryable().OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Dim result = (From p In query Select p)

        Return result.ToList
    End Function

    Public Function ValidateGroupList(ByVal _validate As GroupDTO) As Boolean
        Dim query

        If _validate.CODE <> Nothing Then
            query = (From p In Context.SE_GROUP Where p.CODE.ToUpper = _validate.CODE.ToUpper AndAlso p.ID <> _validate.ID).FirstOrDefault
            Return (query Is Nothing)
        End If
        If _validate.NAME <> Nothing Then
            query = (From p In Context.SE_GROUP Where p.NAME.ToUpper = _validate.NAME.ToUpper AndAlso p.ID <> _validate.ID).FirstOrDefault
            Return (query Is Nothing)
        End If
        Return True
    End Function

    Public Function InsertGroup(ByVal _group As GroupDTO, ByVal log As UserLog) As Boolean
        Try
            Dim _new As New SE_GROUP
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP.EntitySet.Name)
            _new.CODE = _group.CODE
            _new.NAME = _group.NAME
            _new.IS_ADMIN = _group.IS_ADMIN
            _new.ACTFLG = "A"
            _new.EFFECT_DATE = _group.EFFECT_DATE
            _new.EXPIRE_DATE = _group.EXPIRE_DATE
            Context.SE_GROUP.AddObject(_new)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateGroup(ByVal _group As GroupDTO, ByVal log As UserLog) As Boolean
        Try

            Dim lstGroup As SE_GROUP = (From p In Context.SE_GROUP Select p Where p.ID = _group.ID).FirstOrDefault
            If lstGroup IsNot Nothing Then
                lstGroup.NAME = _group.NAME
                lstGroup.IS_ADMIN = _group.IS_ADMIN
                lstGroup.CODE = _group.CODE
                lstGroup.EFFECT_DATE = _group.EFFECT_DATE
                lstGroup.EXPIRE_DATE = _group.EXPIRE_DATE
                Context.SaveChanges(log)
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteGroup(ByVal _lstID As List(Of Decimal), ByRef _error As String, ByVal log As UserLog) As Boolean
        Try
            _error = ""
            Dim lstGroup As List(Of SE_GROUP) = (From p In Context.SE_GROUP Select p Where _lstID.Contains(p.ID)).ToList
            Dim lstDeletes As New List(Of SE_GROUP)
            For i As Integer = 0 To lstGroup.Count - 1
                If lstGroup(i).SE_USERS.Count > 0 Then
                    If _error = "" Then
                        _error = lstGroup(i).NAME
                    Else
                        _error = _error & "," & lstGroup(i).NAME
                    End If

                    If _lstID.Contains(lstGroup(i).ID) Then
                        _lstID.Remove(lstGroup(i).ID)
                    End If
                Else
                    lstDeletes.Add(lstGroup(i))
                End If
            Next
            Dim lstPermissions As List(Of SE_GROUP_PERMISSION) = (From p In Context.SE_GROUP_PERMISSION Select p Where _lstID.Contains(p.GROUP_ID)).ToList
            If lstGroup.Count > 0 Then
                For i = 0 To lstPermissions.Count - 1
                    Context.SE_GROUP_PERMISSION.DeleteObject(lstPermissions(i))
                Next
                For i = 0 To lstDeletes.Count - 1
                    lstDeletes(i).SE_REPORTS.Clear()
                    Context.SE_GROUP.DeleteObject(lstDeletes(i))
                Next
                Context.SaveChanges(log)
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
        Return False
    End Function
    Public Function UpdateGroupStatus(ByVal _lstID As List(Of Decimal), ByVal _ACTFLG As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstGroup As List(Of SE_GROUP) = (From p In Context.SE_GROUP Select p Where _lstID.Contains(p.ID)).ToList
            If lstGroup.Count > 0 Then
                For i = 0 To lstGroup.Count - 1
                    lstGroup(i).ACTFLG = _ACTFLG
                    lstGroup(i).MODIFIED_DATE = DateTime.Now
                    lstGroup(i).MODIFIED_BY = log.Username
                    lstGroup(i).MODIFIED_LOG = log.ComputerName
                Next
                Context.SaveChanges(log)
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Function List"

    Public Function GetFunctionList(ByVal _filter As FunctionDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        Dim query = (From p In Context.SE_FUNCTION
                      Select New FunctionDTO With {
                     .ID = p.ID,
                     .FID = p.FID,
                     .NAME = p.NAME,
                     .MODULE_NAME = p.SE_MODULE.NAME,
                     .FUNCTION_GROUP_ID = p.SE_FUNCTION_GROUP.ID,
                     .FUNCTION_GROUP_NAME = p.SE_FUNCTION_GROUP.NAME,
                     .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Không áp dụng"),
                     .MODULE_ID = p.MODULE_ID})
        If _filter.ID <> 0 Then
            query = query.Where(Function(p) p.ID = _filter.ID)
        End If
        If _filter.FID <> "" Then
            query = query.Where(Function(p) p.FID.ToUpper.Contains(_filter.FID.ToUpper))
        End If
        If _filter.NAME <> "" Then
            query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
        End If
        If _filter.MODULE_ID <> 0 Then
            query = query.Where(Function(p) p.MODULE_ID = _filter.MODULE_ID)
        End If
        If _filter.FUNCTION_GROUP_ID <> 0 Then
            query = query.Where(Function(p) p.FUNCTION_GROUP_ID = _filter.FUNCTION_GROUP_ID)
        End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
        End If
        If PageSize <> -1 Then
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
        End If
        Dim result = (From p In query Select p)
        Return result.ToList
    End Function

    Public Function ValidateFunctionList(ByVal _validate As FunctionDTO) As Boolean
        Dim query
        If _validate.NAME <> "" Then
            query = (From p In Context.SE_FUNCTION Where p.NAME.ToUpper = _validate.NAME.ToUpper).FirstOrDefault
            Return (query Is Nothing)
        End If
        If _validate.FID <> "" Then
            query = (From p In Context.SE_FUNCTION Where p.FID.ToUpper = _validate.FID.ToUpper).FirstOrDefault
            Return (query Is Nothing)
        End If
        Return True
    End Function

    Public Function UpdateFunctionList(ByVal _function As List(Of FunctionDTO), ByVal log As UserLog) As Boolean
        Dim i As Integer
        For i = 0 To _function.Count - 1
            Dim obj As New SE_FUNCTION With {.ID = _function(0).ID}

            Context.SE_FUNCTION.Attach(obj)
            obj.NAME = _function(i).NAME
            obj.FID = _function(i).FID
            obj.GROUP_ID = _function(i).FUNCTION_GROUP_ID
            obj.MODULE_ID = _function(i).MODULE_ID
            obj.MODIFIED_DATE = DateTime.Now
            obj.MODIFIED_BY = log.Username
            obj.MODIFIED_LOG = log.ComputerName
            Context.SaveChanges(log)

            'Dim Decimal_id As Decimal = _function(i).ID
            'Dim query As SE_FUNCTION = (From p In Context.SE_FUNCTION Where p.ID = Decimal_id Select p).FirstOrDefault
        Next
        Return True
    End Function

    Public Function InsertFunctionList(ByVal _item As FunctionDTO, ByVal log As UserLog) As Boolean
        Try
            Dim _new As New SE_FUNCTION
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_FUNCTION.EntitySet.Name)
            _new.ACTFLG = _item.ACTFLG
            _new.NAME = _item.NAME
            _new.FID = _item.FID
            _new.GROUP_ID = _item.FUNCTION_GROUP_ID
            _new.MODULE_ID = _item.MODULE_ID
            _new.MODIFIED_DATE = DateTime.Now
            _new.MODIFIED_BY = log.Username
            _new.MODIFIED_LOG = log.ComputerName
            Context.SE_FUNCTION.AddObject(_new)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetModuleList() As List(Of ModuleDTO)
        Dim query = (From p In Context.SE_MODULE Select New ModuleDTO With {
                     .ID = p.ID,
                     .NAME = p.NAME,
                     .MID = p.MID})
        Return query.ToList
    End Function

    Public Function ActiveFunctions(ByVal lstFunction As List(Of FunctionDTO), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstFunctionData As List(Of SE_FUNCTION)
            Dim lstIDFunction As List(Of Decimal) = (From p In lstFunction.ToList Select p.ID).ToList
            lstFunctionData = (From p In Context.SE_FUNCTION Where lstIDFunction.Contains(p.ID)).ToList
            For index = 0 To lstFunctionData.Count - 1
                lstFunctionData(index).ACTFLG = sActive
                lstFunctionData(index).MODIFIED_DATE = DateTime.Now
                lstFunctionData(index).MODIFIED_BY = log.Username
                lstFunctionData(index).MODIFIED_LOG = log.ComputerName
                Return False
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "Group User"

    Public Function GetUserListInGroup(ByVal _groupID As Decimal,
                                       ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)

        Dim query = (From p In Context.SE_USER
                     From m In p.SE_GROUPS
                     Where m.ID = _groupID
                     Select p)

        If _filter.USERNAME <> "" Then
            query = query.Where(Function(p) p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper))
        End If
        If _filter.FULLNAME <> "" Then
            query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
        End If
        If _filter.EMAIL <> "" Then
            query = query.Where(Function(p) p.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper))
        End If
        If _filter.TELEPHONE <> "" Then
            query = query.Where(Function(p) p.TELEPHONE.ToUpper.Contains(_filter.TELEPHONE.ToUpper))
        End If
        If _filter.IS_APP IsNot Nothing Then
            query = query.Where(Function(p) p.IS_APP = _filter.IS_APP)
        End If
        If _filter.IS_PORTAL IsNot Nothing Then
            query = query.Where(Function(p) p.IS_PORTAL = _filter.IS_PORTAL)
        End If
        If _filter.IS_AD IsNot Nothing Then
            query = query.Where(Function(p) p.IS_AD = _filter.IS_AD)
        End If

        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Return (From p In query
                Select New UserDTO With {
                .ID = p.ID,
                .USERNAME = p.USERNAME,
                .FULLNAME = p.FULLNAME,
                .EMAIL = p.EMAIL,
                .TELEPHONE = p.TELEPHONE,
                .IS_AD = p.IS_AD,
                .IS_APP = p.IS_APP,
                .IS_PORTAL = p.IS_PORTAL}).ToList

    End Function

    Public Function GetUserListOutGroup(ByVal _groupID As Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Dim userIn = (From p In Context.SE_USER
                From m In p.SE_GROUPS
                Where m.ID = _groupID
                Select p)
        Dim query = (From p In Context.SE_USER Where p.ACTFLG.ToUpper = "A" Select p)
        If _filter.USERNAME <> "" Then
            query = query.Where(Function(p) p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper))
        End If
        If _filter.FULLNAME <> "" Then
            query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
        End If
        If _filter.IS_APP IsNot Nothing Then
            query = query.Where(Function(p) p.IS_APP = _filter.IS_APP)
        End If
        If _filter.IS_PORTAL IsNot Nothing Then
            query = query.Where(Function(p) p.IS_PORTAL = _filter.IS_PORTAL)
        End If
        If _filter.IS_AD IsNot Nothing Then
            query = query.Where(Function(p) p.IS_AD = _filter.IS_AD)
        End If
        query = query.Except(userIn)

        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Dim lst = (From p In query
                   Select New UserDTO With {
                .ID = p.ID,
                .USERNAME = p.USERNAME,
                .FULLNAME = p.FULLNAME,
                .EMAIL = p.EMAIL,
                .TELEPHONE = p.TELEPHONE,
                .IS_AD = p.IS_AD,
                .IS_APP = p.IS_APP,
                .IS_PORTAL = p.IS_PORTAL})
        Return lst.ToList
    End Function

    Public Function InsertUserGroup(ByVal _groupID As Decimal, ByVal _lstUserID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstGroup As SE_GROUP = (From p In Context.SE_GROUP Select p Where p.ID = _groupID).FirstOrDefault
        For i As Integer = 0 To _lstUserID.Count - 1
            Dim id As Decimal = _lstUserID(i)
            Dim user As SE_USER = (From p In Context.SE_USER Where p.ID = id Select p).FirstOrDefault
            lstGroup.SE_USERS.Add(user)
        Next
        Context.SaveChanges(log)
        Using conMng As New ConnectionManager
            Using conn As New OracleConnection(conMng.GetConnectionString())
                conn.Open()
                Using cmd As New OracleCommand()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    Try
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PKG_COMMON_BUSINESS.TRANSFER_GROUP_TO_USER"
                        For i As Integer = 0 To _lstUserID.Count - 1
                            cmd.Parameters.Clear()
                            Using resource As New DataAccess.OracleCommon
                                Dim objParam = New With {.P_USER_ID = _lstUserID(i),
                                                         .P_GROUP_ID = _groupID,
                                                         .P_USERNAME = log.Username}

                                If objParam IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If
                                cmd.ExecuteNonQuery()
                            End Using
                        Next
                        cmd.Transaction.Commit()
                    Catch ex As Exception
                        cmd.Transaction.Rollback()
                    Finally
                        'Dispose all resource
                        cmd.Dispose()
                        conn.Close()
                        conn.Dispose()
                    End Try
                End Using
            End Using
        End Using

        Return True
    End Function

    Public Function DeleteUserGroup(ByVal _groupID As Decimal, ByVal _lstUserID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstGroup As SE_GROUP = (From p In Context.SE_GROUP Select p Where p.ID = _groupID).FirstOrDefault
        If lstGroup IsNot Nothing Then
            For i As Integer = 0 To _lstUserID.Count - 1
                Dim user As SE_USER = (From p In lstGroup.SE_USERS.ToList Where p.ID = _lstUserID(i) Select p).FirstOrDefault
                lstGroup.SE_USERS.Remove(user)
            Next
            Context.SaveChanges(log)
        Else
            Return False
        End If
        Return True
    End Function

    Public Function SendMailConfirmUserPassword(ByVal _userid As List(Of Decimal),
                                                ByVal _subject As String,
                                                ByVal _content As String) As Boolean
        Try
            Dim defaultFrom As String = ""
            Dim config As Dictionary(Of String, String)
            config = GetConfig(ModuleID.All)
            defaultFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")

            If defaultFrom = "" Then
                Return False
            End If
            'Lấy danh sách user
            Dim lst As List(Of UserDTO) = (From p In Context.SE_USER
                                       Where _userid.Contains(p.ID) And p.IS_CHANGE_PASS >= 0 And p.EMAIL IsNot Nothing
                                       Select New UserDTO With {
                                           .FULLNAME = p.FULLNAME,
                                           .EMAIL = p.EMAIL,
                                           .PASSWORD = p.PASSWORD}).ToList
            For Each user As UserDTO In lst
                Using EncryptData As New EncryptData
                    If user.EMAIL <> "" AndAlso RegularExpressions.Regex.IsMatch(user.EMAIL, "^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$") Then
                        InsertMail(defaultFrom, user.EMAIL, _subject, String.Format(_content, EncryptData.DecryptString(user.PASSWORD)))
                    End If
                End Using
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUserNeedSendMail(ByVal _groupid As Decimal) As List(Of UserDTO)
        Try
            Dim lst As List(Of UserDTO) = (From p In Context.SE_USER
                                           From n In p.SE_GROUPS
                                           Where n.ID = _groupid And p.IS_CHANGE_PASS >= 0
                                           Select New UserDTO With {
                                               .ID = p.ID,
                                               .FULLNAME = p.FULLNAME,
                                               .USERNAME = p.USERNAME,
                                               .IS_CHANGE_PASS = p.IS_CHANGE_PASS,
                                               .EMAIL = p.EMAIL}).ToList
            For i = lst.Count - 1 To 0 Step -1
                If lst(i).EMAIL = "" OrElse Not RegularExpressions.Regex.IsMatch(lst(i).EMAIL, "^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$") Then
                    lst.RemoveAt(i)
                End If
            Next
            Return lst
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Group Function"

    Public Function GetGroupFunctionPermision(ByVal _groupID As Decimal,
                                                ByVal _filter As GroupFunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "FUNCTION_NAME asc") As List(Of GroupFunctionDTO)
        Dim query = (From p In Context.SE_GROUP_PERMISSION
                     Where p.GROUP_ID = _groupID
                     Select New GroupFunctionDTO With {
                         .ID = p.ID,
                         .FUNCTION_ID = p.FUNCTION_ID,
                         .FUNCTION_CODE = p.SE_FUNCTION.FID,
                         .FUNCTION_NAME = p.SE_FUNCTION.NAME,
                         .GROUP_ID = p.GROUP_ID,
                         .MODULE_NAME = p.SE_FUNCTION.SE_MODULE.NAME,
                         .ALLOW_CREATE = p.ALLOW_CREATE,
                         .ALLOW_DELETE = p.ALLOW_DELETE,
                         .ALLOW_EXPORT = p.ALLOW_EXPORT,
                         .ALLOW_IMPORT = p.ALLOW_IMPORT,
                         .ALLOW_MODIFY = p.ALLOW_MODIFY,
                         .ALLOW_PRINT = p.ALLOW_PRINT,
                         .ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1,
                         .ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2,
                         .ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3,
                         .ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4,
                         .ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5})

        If _filter.FUNCTION_NAME <> "" Then
            query = query.Where(Function(p) p.FUNCTION_NAME.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper) Or
                                    p.FUNCTION_CODE.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper))
        End If

        If _filter.MODULE_NAME <> "" Then
            query = query.Where(Function(p) p.MODULE_NAME = _filter.MODULE_NAME)
        End If

        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)
        Return query.ToList
    End Function

    Public Function GetGroupFunctionNotPermision(ByVal _groupID As Decimal,
                                                ByVal _filter As FunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                ByVal log As UserLog,
                                                Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)

        Dim lstTemp As New List(Of SE_FUNCTION)
        If log.Username = "ADMIN" Then
            lstTemp = (From p In Context.SE_FUNCTION Select p).ToList
        Else
            lstTemp = (From p In Context.SE_FUNCTION
                       From q In Context.SE_USER_PERMISSION.Where(Function(f) f.FUNCTION_ID = p.ID)
                       From u In Context.SE_USER.Where(Function(f) f.ID = q.USER_ID And f.USERNAME = log.Username)
                       Select p).ToList
        End If

        If _filter.MODULE_NAME <> "" Then
            lstTemp = lstTemp.Where(Function(p) p.SE_MODULE.NAME.ToUpper.Contains(_filter.MODULE_NAME.ToUpper))
        End If
        If _filter.FUNCTION_GROUP_NAME <> "" Then
            lstTemp = lstTemp.Where(Function(p) p.SE_FUNCTION_GROUP.NAME.ToUpper.Contains(_filter.FUNCTION_GROUP_NAME.ToUpper))
        End If
        If _filter.NAME <> "" Then
            lstTemp = lstTemp.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
        End If
        Dim lst1 = (From p In lstTemp Select p.ID)
        Dim lst2 = (From p In Context.SE_GROUP_PERMISSION Where p.GROUP_ID = _groupID Select p.FUNCTION_ID)
        Dim lstID As List(Of Decimal) = lst1.Except(lst2).ToList

        Dim query = (From p In Context.SE_FUNCTION Where lstID.Contains(p.ID) Order By p.NAME Select New FunctionDTO With {
                                                    .ID = p.ID,
                                                    .NAME = p.NAME,
                                                    .MODULE_NAME = p.SE_MODULE.NAME,
                                                    .FID = p.FID,
                                                    .FUNCTION_GROUP_NAME = p.SE_FUNCTION_GROUP.NAME})

        query = query.AsQueryable().OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)
        Return query.ToList
    End Function

    Public Function InsertGroupFunction(ByVal _lstGroupFunc As List(Of GroupFunctionDTO), ByVal log As UserLog) As Boolean
        For i As Integer = 0 To _lstGroupFunc.Count - 1

            Dim itemAdd = _lstGroupFunc(i)

            Dim functionCheck = Context.SE_GROUP_PERMISSION.FirstOrDefault(Function(p) p.FUNCTION_ID = itemAdd.FUNCTION_ID AndAlso p.GROUP_ID = itemAdd.GROUP_ID)

            If functionCheck IsNot Nothing Then Continue For

            Dim _new As New SE_GROUP_PERMISSION
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP_PERMISSION.EntitySet.Name)
            _new.ALLOW_CREATE = _lstGroupFunc(i).ALLOW_CREATE
            _new.ALLOW_MODIFY = _lstGroupFunc(i).ALLOW_MODIFY
            _new.ALLOW_DELETE = _lstGroupFunc(i).ALLOW_DELETE
            _new.ALLOW_PRINT = _lstGroupFunc(i).ALLOW_PRINT
            _new.ALLOW_IMPORT = _lstGroupFunc(i).ALLOW_IMPORT
            _new.ALLOW_EXPORT = _lstGroupFunc(i).ALLOW_EXPORT
            _new.ALLOW_SPECIAL1 = _lstGroupFunc(i).ALLOW_SPECIAL1
            _new.ALLOW_SPECIAL2 = _lstGroupFunc(i).ALLOW_SPECIAL2
            _new.ALLOW_SPECIAL3 = _lstGroupFunc(i).ALLOW_SPECIAL3
            _new.ALLOW_SPECIAL4 = _lstGroupFunc(i).ALLOW_SPECIAL4
            _new.ALLOW_SPECIAL5 = _lstGroupFunc(i).ALLOW_SPECIAL5
            _new.FUNCTION_ID = _lstGroupFunc(i).FUNCTION_ID
            _new.GROUP_ID = _lstGroupFunc(i).GROUP_ID
            _new.CREATED_DATE = DateTime.Now
            _new.CREATED_BY = log.Username
            _new.CREATED_LOG = log.ComputerName
            _new.MODIFIED_DATE = DateTime.Now
            _new.MODIFIED_BY = log.Username
            _new.MODIFIED_LOG = log.ComputerName
            Context.SE_GROUP_PERMISSION.AddObject(_new)
        Next
        Context.SaveChanges(log)
        For i As Integer = 0 To _lstGroupFunc.Count - 1

            'Dim itemAdd = _lstGroupFunc(i)
            'lay ds ú theo _lstGroupFunc(i).GROUP_ID
            'for ds us

            Dim lst = _lstGroupFunc(i).GROUP_ID
            Dim query = (From p In Context.HUV_SE_GRP_SE_USR Where p.SE_GROUPS_ID = lst
            Select New UserDTO With {.ID = p.SE_USERS_ID}).ToList()
            For Each ITEM In query
                Dim _NEW1 As New SE_USER_PERMISSION
                _NEW1.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION.EntitySet.Name)
                _NEW1.ALLOW_CREATE = _lstGroupFunc(i).ALLOW_CREATE
                _NEW1.ALLOW_MODIFY = _lstGroupFunc(i).ALLOW_MODIFY
                _NEW1.ALLOW_DELETE = _lstGroupFunc(i).ALLOW_DELETE
                _NEW1.ALLOW_PRINT = _lstGroupFunc(i).ALLOW_PRINT
                _NEW1.ALLOW_IMPORT = _lstGroupFunc(i).ALLOW_IMPORT
                _NEW1.ALLOW_EXPORT = _lstGroupFunc(i).ALLOW_EXPORT
                _NEW1.ALLOW_SPECIAL1 = _lstGroupFunc(i).ALLOW_SPECIAL1
                _NEW1.ALLOW_SPECIAL2 = _lstGroupFunc(i).ALLOW_SPECIAL2
                _NEW1.ALLOW_SPECIAL3 = _lstGroupFunc(i).ALLOW_SPECIAL3
                _NEW1.ALLOW_SPECIAL4 = _lstGroupFunc(i).ALLOW_SPECIAL4
                _NEW1.ALLOW_SPECIAL5 = _lstGroupFunc(i).ALLOW_SPECIAL5
                _NEW1.FUNCTION_ID = _lstGroupFunc(i).FUNCTION_ID
                _NEW1.GROUP_ID = _lstGroupFunc(i).GROUP_ID
                _NEW1.USER_ID = ITEM.ID
                _NEW1.CREATED_DATE = DateTime.Now
                _NEW1.CREATED_BY = log.Username
                _NEW1.CREATED_LOG = log.ComputerName
                _NEW1.MODIFIED_DATE = DateTime.Now
                _NEW1.MODIFIED_BY = log.Username
                _NEW1.MODIFIED_LOG = log.ComputerName
                Context.SE_USER_PERMISSION.AddObject(_NEW1)
            Next
            'For Each item In query
            '    Dim a = item
            'Next



        Next

        Context.SaveChanges(log)
        Return True
    End Function

    Public Function UpdateGroupFunction(ByVal _lstGroupFunc As List(Of GroupFunctionDTO), ByVal log As UserLog) As Boolean
        Dim i As Integer
        Dim lstID As List(Of Decimal) = (From p In _lstGroupFunc Select p.ID).ToList
        Dim objUpdate As List(Of SE_GROUP_PERMISSION) = (From p In Context.SE_GROUP_PERMISSION Where lstID.Contains(p.ID) Select p).ToList
        For i = 0 To objUpdate.Count - 1
            Dim func As GroupFunctionDTO = _lstGroupFunc.Find(Function(item As GroupFunctionDTO) item.ID = objUpdate(i).ID)
            If func IsNot Nothing Then
                objUpdate(i).ALLOW_CREATE = If(func.ALLOW_CREATE, 1, 0)
                objUpdate(i).ALLOW_DELETE = If(func.ALLOW_DELETE, 1, 0)
                objUpdate(i).ALLOW_EXPORT = If(func.ALLOW_EXPORT, 1, 0)
                objUpdate(i).ALLOW_IMPORT = If(func.ALLOW_IMPORT, 1, 0)
                objUpdate(i).ALLOW_MODIFY = If(func.ALLOW_MODIFY, 1, 0)
                objUpdate(i).ALLOW_PRINT = If(func.ALLOW_PRINT, 1, 0)
                objUpdate(i).ALLOW_SPECIAL1 = If(func.ALLOW_SPECIAL1, 1, 0)
                objUpdate(i).ALLOW_SPECIAL2 = If(func.ALLOW_SPECIAL2, 1, 0)
                objUpdate(i).ALLOW_SPECIAL3 = If(func.ALLOW_SPECIAL3, 1, 0)
                objUpdate(i).ALLOW_SPECIAL4 = If(func.ALLOW_SPECIAL4, 1, 0)
                objUpdate(i).ALLOW_SPECIAL5 = If(func.ALLOW_SPECIAL5, 1, 0)
                objUpdate(i).MODIFIED_DATE = DateTime.Now
                objUpdate(i).MODIFIED_BY = log.Username
                objUpdate(i).MODIFIED_LOG = log.ComputerName
            End If
        Next
        Context.SaveChanges(log)
        For i = 0 To _lstGroupFunc.Count - 1
            Dim j As Integer
            Dim lst = _lstGroupFunc(i).GROUP_ID
            Dim lst1 = _lstGroupFunc(i).FUNCTION_ID
            Dim query As List(Of UserFunctionDTO) = (From p In Context.HUV_SE_GRP_SE_USR
                        From q In Context.SE_USER_PERMISSION.Where(Function(f) f.USER_ID = p.SE_USERS_ID).DefaultIfEmpty
                         Where p.SE_GROUPS_ID = lst And q.FUNCTION_ID = lst1 Select New UserFunctionDTO With {.ID = q.ID}).ToList

            'Dim _lstUserFunc As List(Of UserFunctionDTO) = Decimal.Parse(query.ToString)
            Dim lstID1 As List(Of Decimal) = (From p In query Select p.ID).ToList
            Dim objUpdate1 As List(Of SE_USER_PERMISSION) = (From p In Context.SE_USER_PERMISSION Where lstID1.Contains(p.ID) Select p).ToList
            For j = 0 To objUpdate1.Count - 1
                'Dim func As UserFunctionDTO = query.Find(Function(item As UserFunctionDTO) item.ID = objUpdate(i).ID)
                'Dim func1 As UserFunctionDTO = query.Find(Function(e As UserFunctionDTO) e.ID = objUpdate1(j).ID)
                Dim func As GroupFunctionDTO = _lstGroupFunc.Find(Function(item As GroupFunctionDTO) item.ID = objUpdate(i).ID)
                If func IsNot Nothing Then
                    objUpdate1(j).ALLOW_CREATE = If(func.ALLOW_CREATE, 1, 0)
                    objUpdate1(j).ALLOW_DELETE = If(func.ALLOW_DELETE, 1, 0)
                    objUpdate1(j).ALLOW_EXPORT = If(func.ALLOW_EXPORT, 1, 0)
                    objUpdate1(j).ALLOW_IMPORT = If(func.ALLOW_IMPORT, 1, 0)
                    objUpdate1(j).ALLOW_MODIFY = If(func.ALLOW_MODIFY, 1, 0)
                    objUpdate1(j).ALLOW_PRINT = If(func.ALLOW_PRINT, 1, 0)
                    objUpdate1(j).ALLOW_SPECIAL1 = If(func.ALLOW_SPECIAL1, 1, 0)
                    objUpdate1(j).ALLOW_SPECIAL2 = If(func.ALLOW_SPECIAL2, 1, 0)
                    objUpdate1(j).ALLOW_SPECIAL3 = If(func.ALLOW_SPECIAL3, 1, 0)
                    objUpdate1(j).ALLOW_SPECIAL4 = If(func.ALLOW_SPECIAL4, 1, 0)
                    objUpdate1(j).ALLOW_SPECIAL5 = If(func.ALLOW_SPECIAL5, 1, 0)
                End If
            Next
        Next
        Context.SaveChanges()
        Return True
    End Function

    Public Function DeleteGroupFunction(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            'Using conMng As New ConnectionManager
            '    Using conn As New OracleConnection(conMng.GetConnectionString())
            '        conn.Open()
            '        Using cmd As New OracleCommand()
            '            Try
            '                cmd.Connection = conn
            '                cmd.Transaction = cmd.Connection.BeginTransaction()
            '                For i As Integer = 0 To _lstID.Count - 1
            '                    cmd.CommandText = "DELETE SE_GROUP_PERMISSION WHERE ID =" & _lstID(i)
            '                    cmd.ExecuteNonQuery()
            '                Next
            '                cmd.Transaction.Commit()
            '            Catch ex As Exception
            '                cmd.Transaction.Rollback()
            '            Finally
            '                'Dispose all resource
            '                cmd.Dispose()
            '                conn.Close()
            '                conn.Dispose()
            '            End Try
            '        End Using
            '    End Using
            'End Using
            Dim str As String = ""
            For i As Integer = 0 To _lstID.Count - 1
                str += _lstID(i) & ";"
            Next
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.DELETE_GROUP_FUNCTION",
                                New With {.P_LIST_ID = str})
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function CopyGroupFunction(ByVal groupCopyID As Decimal, ByVal groupID As Decimal, ByVal log As UserLog) As Boolean
        ' bổ sung delete phục vụ cho chức năng copy function
        Dim lstDel = (From p In Context.SE_GROUP_PERMISSION Where p.GROUP_ID = groupID).ToList
        For Each itm In lstDel
            Context.SE_GROUP_PERMISSION.DeleteObject(itm)
        Next
        Context.SaveChanges(log)

        Dim _lstGroupFunc = (From p In Context.SE_GROUP_PERMISSION Where p.GROUP_ID = groupCopyID
                             Select New GroupFunctionDTO With {.GROUP_ID = groupID,
                                                               .FUNCTION_ID = p.FUNCTION_ID,
                                                              .ALLOW_CREATE = p.ALLOW_CREATE,
                                                               .ALLOW_DELETE = p.ALLOW_DELETE,
                                                               .ALLOW_EXPORT = p.ALLOW_EXPORT,
                                                               .ALLOW_IMPORT = p.ALLOW_IMPORT,
                                                               .ALLOW_MODIFY = p.ALLOW_MODIFY,
                                                               .ALLOW_PRINT = p.ALLOW_PRINT,
                                                               .ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1,
                                                               .ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2,
                                                               .ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3,
                                                               .ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4,
                                                               .ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5}).ToList
        For i As Integer = 0 To _lstGroupFunc.Count - 1

            Dim itemAdd = _lstGroupFunc(i)

            Dim functionCheck = Context.SE_GROUP_PERMISSION.FirstOrDefault(Function(p) p.FUNCTION_ID = itemAdd.FUNCTION_ID AndAlso p.GROUP_ID = itemAdd.GROUP_ID)

            If functionCheck IsNot Nothing Then Continue For

            Dim _new As New SE_GROUP_PERMISSION
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP_PERMISSION.EntitySet.Name)
            _new.ALLOW_CREATE = _lstGroupFunc(i).ALLOW_CREATE
            _new.ALLOW_MODIFY = _lstGroupFunc(i).ALLOW_MODIFY
            _new.ALLOW_DELETE = _lstGroupFunc(i).ALLOW_DELETE
            _new.ALLOW_PRINT = _lstGroupFunc(i).ALLOW_PRINT
            _new.ALLOW_IMPORT = _lstGroupFunc(i).ALLOW_IMPORT
            _new.ALLOW_EXPORT = _lstGroupFunc(i).ALLOW_EXPORT
            _new.ALLOW_SPECIAL1 = _lstGroupFunc(i).ALLOW_SPECIAL1
            _new.ALLOW_SPECIAL2 = _lstGroupFunc(i).ALLOW_SPECIAL2
            _new.ALLOW_SPECIAL3 = _lstGroupFunc(i).ALLOW_SPECIAL3
            _new.ALLOW_SPECIAL4 = _lstGroupFunc(i).ALLOW_SPECIAL4
            _new.ALLOW_SPECIAL5 = _lstGroupFunc(i).ALLOW_SPECIAL5
            _new.FUNCTION_ID = _lstGroupFunc(i).FUNCTION_ID
            _new.GROUP_ID = _lstGroupFunc(i).GROUP_ID
            Context.SE_GROUP_PERMISSION.AddObject(_new)
            Context.SaveChanges(log)
        Next
        Return True
    End Function

#End Region

#Region "Group Report"

    Public Function GetGroupReportList(ByVal _groupID As Decimal) As List(Of GroupReportDTO)
        Dim query = (From p In Context.SE_REPORT Order By p.NAME
                     Select New GroupReportDTO With {
                         .ID = p.ID,
                         .REPORT_NAME = p.NAME,
                         .MODULE_NAME = p.SE_MODULE.NAME,
                         .IS_USE = ((From n In p.SE_GROUPS Where n.ID = _groupID).Count > 0)})
        Return query.ToList
    End Function

    Public Function GetGroupReportListFilter(ByVal _groupID As Decimal, ByVal _filter As GroupReportDTO) As List(Of GroupReportDTO)
        Dim query = (From p In Context.SE_REPORT Order By p.NAME
                     Where (_filter.REPORT_NAME = Nothing Or p.NAME.ToUpper = _filter.REPORT_NAME.ToUpper) And
                     (_filter.MODULE_ID = Nothing Or p.SE_MODULE.ID = _filter.MODULE_ID)
                     Select New GroupReportDTO With {
                         .ID = p.ID,
                         .REPORT_NAME = p.NAME,
                         .MODULE_NAME = p.SE_MODULE.NAME,
                         .IS_USE = ((From n In p.SE_GROUPS Where n.ID = _groupID).Count > 0)})
        Return query.ToList
    End Function

    Public Function UpdateGroupReport(ByVal _groupID As Decimal, ByVal _lstReport As List(Of GroupReportDTO)) As Boolean
        Dim query As SE_GROUP
        query = (From p In Context.SE_GROUP
                 Where p.ID = _groupID
                 Select p).FirstOrDefault
        Dim Ids As List(Of Decimal) = (From p In _lstReport Select p.ID).ToList
        Dim lst = (From p In Context.SE_REPORT Where Ids.Contains(p.ID)).ToList
        If query IsNot Nothing Then
            query.SE_REPORTS.Clear()
            For i As Integer = 0 To lst.Count - 1
                query.SE_REPORTS.Add(lst(i))
            Next
            Context.SaveChanges()
        Else
            Return False
        End If
        Return True
    End Function

#End Region

#Region "User Function"

    Public Function GetUserFunctionPermision(ByVal _UserID As Decimal,
                                                ByVal _filter As UserFunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "FUNCTION_NAME asc") As List(Of UserFunctionDTO)
        Try
            Dim query = (From p In Context.SE_USER_PERMISSION
                         From f In Context.SE_FUNCTION.Where(Function(f) f.ID = p.FUNCTION_ID)
                         Where p.USER_ID = _UserID
                         Select New UserFunctionDTO With {
                             .ID = p.ID,
                             .FUNCTION_ID = p.FUNCTION_ID,
                             .FUNCTION_CODE = f.FID,
                             .FUNCTION_NAME = f.NAME,
                             .USER_ID = p.USER_ID,
                             .MODULE_NAME = f.SE_MODULE.NAME,
                             .ALLOW_CREATE = p.ALLOW_CREATE,
                             .ALLOW_DELETE = p.ALLOW_DELETE,
                             .ALLOW_EXPORT = p.ALLOW_EXPORT,
                             .ALLOW_IMPORT = p.ALLOW_IMPORT,
                             .ALLOW_MODIFY = p.ALLOW_MODIFY,
                             .ALLOW_PRINT = p.ALLOW_PRINT,
                             .ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1,
                             .ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2,
                             .ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3,
                             .ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4,
                             .ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5})

            If _filter.FUNCTION_NAME <> "" Then
                query = query.Where(Function(p) p.FUNCTION_NAME.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper) Or
                                        p.FUNCTION_CODE.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper))
            End If

            If _filter.MODULE_NAME <> "" Then
                query = query.Where(Function(p) p.MODULE_NAME = _filter.MODULE_NAME)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUserFunctionNotPermision(ByVal _UserID As Decimal,
                                                ByVal _filter As FunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                ByVal log As UserLog,
                                                Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        Try
            Dim lstTemp As New List(Of SE_FUNCTION)
            If log.Username = "ADMIN" Then
                lstTemp = (From p In Context.SE_FUNCTION Select p).ToList
            Else
                lstTemp = (From p In Context.SE_FUNCTION
                           From q In Context.SE_USER_PERMISSION.Where(Function(f) f.FUNCTION_ID = p.ID)
                           From u In Context.SE_USER.Where(Function(f) f.ID = q.USER_ID And f.USERNAME = log.Username)
                           Select p).ToList
            End If


            If _filter.MODULE_NAME <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.SE_MODULE.NAME.ToUpper.Contains(_filter.MODULE_NAME.ToUpper)).ToList
            End If
            If _filter.FUNCTION_GROUP_NAME <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.SE_FUNCTION_GROUP.NAME.ToUpper.Contains(_filter.FUNCTION_GROUP_NAME.ToUpper)).ToList
            End If
            If _filter.NAME <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper)).ToList
            End If
            Dim lst1 = (From p In lstTemp Select p.ID)
            Dim lst2 = (From p In Context.SE_USER_PERMISSION Where p.USER_ID = _UserID Select p.FUNCTION_ID)
            Dim lstID As List(Of Decimal) = lst1.Except(lst2).ToList

            Dim query = (From p In Context.SE_FUNCTION Where lstID.Contains(p.ID)
                         Order By p.NAME
                         Select New FunctionDTO With {
                             .ID = p.ID,
                             .NAME = p.NAME,
                             .MODULE_NAME = p.SE_MODULE.NAME,
                             .FID = p.FID,
                             .FUNCTION_GROUP_NAME = p.SE_FUNCTION_GROUP.NAME})

            query = query.AsQueryable().OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertUserFunction(ByVal _lstUserFunc As List(Of UserFunctionDTO), ByVal log As UserLog) As Boolean
        Try
            For i As Integer = 0 To _lstUserFunc.Count - 1

                Dim itemAdd = _lstUserFunc(i)

                Dim functionCheck = (From p In Context.SE_USER_PERMISSION
                                     Where p.FUNCTION_ID = itemAdd.FUNCTION_ID AndAlso
                                     p.USER_ID = itemAdd.USER_ID).FirstOrDefault

                If functionCheck IsNot Nothing Then Continue For

                Dim _new As New SE_USER_PERMISSION
                _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION.EntitySet.Name)
                _new.ALLOW_CREATE = _lstUserFunc(i).ALLOW_CREATE
                _new.ALLOW_MODIFY = _lstUserFunc(i).ALLOW_MODIFY
                _new.ALLOW_DELETE = _lstUserFunc(i).ALLOW_DELETE
                _new.ALLOW_PRINT = _lstUserFunc(i).ALLOW_PRINT
                _new.ALLOW_IMPORT = _lstUserFunc(i).ALLOW_IMPORT
                _new.ALLOW_EXPORT = _lstUserFunc(i).ALLOW_EXPORT
                _new.ALLOW_SPECIAL1 = _lstUserFunc(i).ALLOW_SPECIAL1
                _new.ALLOW_SPECIAL2 = _lstUserFunc(i).ALLOW_SPECIAL2
                _new.ALLOW_SPECIAL3 = _lstUserFunc(i).ALLOW_SPECIAL3
                _new.ALLOW_SPECIAL4 = _lstUserFunc(i).ALLOW_SPECIAL4
                _new.ALLOW_SPECIAL5 = _lstUserFunc(i).ALLOW_SPECIAL5
                _new.FUNCTION_ID = _lstUserFunc(i).FUNCTION_ID
                _new.USER_ID = _lstUserFunc(i).USER_ID
                Context.SE_USER_PERMISSION.AddObject(_new)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateUserFunction(ByVal _lstUserFunc As List(Of UserFunctionDTO), ByVal log As UserLog) As Boolean
        Try
            Dim i As Integer
            Dim lstID As List(Of Decimal) = (From p In _lstUserFunc Select p.ID).ToList
            Dim objUpdate As List(Of SE_USER_PERMISSION) = (From p In Context.SE_USER_PERMISSION Where lstID.Contains(p.ID) Select p).ToList
            For i = 0 To objUpdate.Count - 1
                Dim func As UserFunctionDTO = _lstUserFunc.Find(Function(item As UserFunctionDTO) item.ID = objUpdate(i).ID)
                If func IsNot Nothing Then
                    objUpdate(i).ALLOW_CREATE = If(func.ALLOW_CREATE, 1, 0)
                    objUpdate(i).ALLOW_DELETE = If(func.ALLOW_DELETE, 1, 0)
                    objUpdate(i).ALLOW_EXPORT = If(func.ALLOW_EXPORT, 1, 0)
                    objUpdate(i).ALLOW_IMPORT = If(func.ALLOW_IMPORT, 1, 0)
                    objUpdate(i).ALLOW_MODIFY = If(func.ALLOW_MODIFY, 1, 0)
                    objUpdate(i).ALLOW_PRINT = If(func.ALLOW_PRINT, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL1 = If(func.ALLOW_SPECIAL1, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL2 = If(func.ALLOW_SPECIAL2, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL3 = If(func.ALLOW_SPECIAL3, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL4 = If(func.ALLOW_SPECIAL4, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL5 = If(func.ALLOW_SPECIAL5, 1, 0)
                End If
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteUserFunction(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    conn.Open()
                    Using cmd As New OracleCommand()
                        Try
                            cmd.Connection = conn
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For i As Integer = 0 To _lstID.Count - 1
                                cmd.CommandText = "DELETE SE_USER_PERMISSION WHERE ID =" & _lstID(i)
                                cmd.ExecuteNonQuery()
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function CopyUserFunction(ByVal UserCopyID As Decimal, ByVal UserID As Decimal, ByVal log As UserLog) As Boolean
        ' bổ sung delete phục vụ cho chức năng copy function
        Dim lstDel = (From p In Context.SE_USER_PERMISSION Where p.USER_ID = UserID).ToList
        For Each itm In lstDel
            Context.SE_USER_PERMISSION.DeleteObject(itm)
        Next
        Context.SaveChanges(log)

        Dim _lstUserFunc = (From p In Context.SE_USER_PERMISSION Where p.USER_ID = UserCopyID
                             Select New UserFunctionDTO With {.USER_ID = UserID,
                                                               .FUNCTION_ID = p.FUNCTION_ID,
                                                              .ALLOW_CREATE = p.ALLOW_CREATE,
                                                               .ALLOW_DELETE = p.ALLOW_DELETE,
                                                               .ALLOW_EXPORT = p.ALLOW_EXPORT,
                                                               .ALLOW_IMPORT = p.ALLOW_IMPORT,
                                                               .ALLOW_MODIFY = p.ALLOW_MODIFY,
                                                               .ALLOW_PRINT = p.ALLOW_PRINT,
                                                               .ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1,
                                                               .ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2,
                                                               .ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3,
                                                               .ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4,
                                                               .ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5}).ToList
        For i As Integer = 0 To _lstUserFunc.Count - 1

            Dim itemAdd = _lstUserFunc(i)

            Dim functionCheck = Context.SE_USER_PERMISSION.FirstOrDefault(Function(p) p.FUNCTION_ID = itemAdd.FUNCTION_ID AndAlso p.USER_ID = itemAdd.USER_ID)

            If functionCheck IsNot Nothing Then Continue For

            Dim _new As New SE_USER_PERMISSION
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION.EntitySet.Name)
            _new.ALLOW_CREATE = _lstUserFunc(i).ALLOW_CREATE
            _new.ALLOW_MODIFY = _lstUserFunc(i).ALLOW_MODIFY
            _new.ALLOW_DELETE = _lstUserFunc(i).ALLOW_DELETE
            _new.ALLOW_PRINT = _lstUserFunc(i).ALLOW_PRINT
            _new.ALLOW_IMPORT = _lstUserFunc(i).ALLOW_IMPORT
            _new.ALLOW_EXPORT = _lstUserFunc(i).ALLOW_EXPORT
            _new.ALLOW_SPECIAL1 = _lstUserFunc(i).ALLOW_SPECIAL1
            _new.ALLOW_SPECIAL2 = _lstUserFunc(i).ALLOW_SPECIAL2
            _new.ALLOW_SPECIAL3 = _lstUserFunc(i).ALLOW_SPECIAL3
            _new.ALLOW_SPECIAL4 = _lstUserFunc(i).ALLOW_SPECIAL4
            _new.ALLOW_SPECIAL5 = _lstUserFunc(i).ALLOW_SPECIAL5
            _new.FUNCTION_ID = _lstUserFunc(i).FUNCTION_ID
            _new.USER_ID = _lstUserFunc(i).USER_ID
            Context.SE_USER_PERMISSION.AddObject(_new)
            Context.SaveChanges(log)
        Next
        Return True
    End Function

#End Region

#Region "User Organization"

    Public Function GetUserOrganization(ByVal _UserID As Decimal) As List(Of Decimal)
        Return (From p In Context.SE_USER_ORG_ACCESS
                From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                Where p.USER_ID = _UserID
                Select p.ORG_ID).ToList
    End Function

    Public Function DeleteUserOrganization(ByVal _UserId As Decimal)
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteSQL("DELETE SE_USER_ORG_ACCESS WHERE USER_ID =" & _UserId)
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateUserOrganization(ByVal _lstOrg As List(Of UserOrgAccessDTO)) As Boolean
        If _lstOrg.Count > 0 Then
            Dim i As Integer
            For i = 0 To _lstOrg.Count - 1
                Dim _item As New SE_USER_ORG_ACCESS
                _item.ID = Utilities.GetNextSequence(Context, Context.SE_USER_ORG_ACCESS.EntitySet.Name)
                _item.USER_ID = _lstOrg(i).USER_ID
                _item.ORG_ID = _lstOrg(i).ORG_ID
                Context.SE_USER_ORG_ACCESS.AddObject(_item)
            Next
            Context.SaveChanges()
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "User Report"

    Public Function GetUserReportList(ByVal _UserID As Decimal) As List(Of UserReportDTO)
        Dim query = (From p In Context.SE_REPORT
                     Order By p.NAME
                     Select New UserReportDTO With {
                         .ID = p.ID,
                         .REPORT_NAME = p.NAME,
                         .MODULE_NAME = p.SE_MODULE.NAME,
                         .IS_USE = ((From n In p.SE_USER Where n.ID = _UserID).Count > 0)})
        Return query.ToList
    End Function

    Public Function GetUserReportListFilter(ByVal _UserID As Decimal, ByVal _filter As UserReportDTO) As List(Of UserReportDTO)
        Dim query = (From p In Context.SE_REPORT
                     Order By p.NAME
                     Where (_filter.REPORT_NAME = Nothing Or p.NAME.ToUpper = _filter.REPORT_NAME.ToUpper) And
                     (_filter.MODULE_ID = Nothing Or p.SE_MODULE.ID = _filter.MODULE_ID)
                     Select New UserReportDTO With {
                         .ID = p.ID,
                         .REPORT_NAME = p.NAME,
                         .MODULE_NAME = p.SE_MODULE.NAME,
                         .IS_USE = ((From n In p.SE_USER Where n.ID = _UserID).Count > 0)})
        Return query.ToList
    End Function

    Public Function UpdateUserReport(ByVal _UserID As Decimal, ByVal _lstReport As List(Of UserReportDTO)) As Boolean
        Dim query As SE_USER
        query = (From p In Context.SE_USER
                 Where p.ID = _UserID
                 Select p).FirstOrDefault

        Dim Ids As List(Of Decimal) = (From p In _lstReport Select p.ID).ToList
        Dim lst = (From p In Context.SE_REPORT Where Ids.Contains(p.ID)).ToList
        If query IsNot Nothing Then
            query.SE_REPORT.Clear()
            For i As Integer = 0 To lst.Count - 1
                query.SE_REPORT.Add(lst(i))
            Next
            Context.SaveChanges()
        Else
            Return False
        End If
        Return True
    End Function

#End Region

#Region "AccessLog"

    Public Function GetAccessLog(ByVal filter As AccessLogFilter,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "LoginDate desc") As List(Of AccessLog)
        Try
            Return AuditLogHelper.GetAccessLog(filter, PageIndex, PageSize, Total, Sorts)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function InsertAccessLog(ByVal _accesslog As AccessLog) As Boolean
        Try
            Return AuditLogHelper.InsertAccessLog(_accesslog)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "ActionLog"

    Public Function GetActionLog(ByVal filter As ActionLogFilter,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "ActionDate desc") As List(Of ActionLog)
        Try
            Return (AuditLogHelper.GetActionLog(filter, PageIndex, PageSize, Total, Sorts))
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetActionLog(ByVal objectId As Decimal) As List(Of ActionLog)
        Try
            Return AuditLogHelper.GetActionLog(objectId)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetActionLogByID(ByVal gID As Decimal) As List(Of AuditLogDtl)
        Try
            Return AuditLogHelper.GetActionLogByID(gID)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteActionLogs(ByVal lstDeleteIds As List(Of Decimal)) As Integer
        Try
            Return AuditLogHelper.DeleteActionLogs(lstDeleteIds)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Dim mydbcontext As New Entity.DbContext(Context, True)

#End Region

#Region "Check User Login"

    Public Function IsUsernameExist(ByVal Username As String) As Boolean
        Dim u As SE_USER = (From p In Context.SE_USER Where p.USERNAME.ToUpper.Equals(Username.ToUpper)).FirstOrDefault
        If u IsNot Nothing Then
            Return True
        End If
        Return False
    End Function

    Public Function GetUserPermissions(ByVal Username As String) As List(Of PermissionDTO)
        Try
            If Username = "" Then Return New List(Of PermissionDTO)
            Dim query = From p In Context.SE_USER.Include("SE_GROUPS") Where p.USERNAME.ToUpper = Username.ToUpper
            'Logger.LogInfo(query)
            Dim u As SE_USER = query.FirstOrDefault
            If u Is Nothing Then Return New List(Of PermissionDTO)
            Dim query1 = From p In u.SE_GROUPS Where p.ACTFLG = "A" And p.EFFECT_DATE <= Date.Now And (p.EXPIRE_DATE Is Nothing OrElse p.EXPIRE_DATE >= Date.Now) Select p.ID
            'Logger.LogInfo(query1)
            Dim lstGroupIds As List(Of Decimal) = query1.ToList

            Dim query2 = From p In Context.SE_USER_PERMISSION
                         From func In Context.SE_FUNCTION.Where(Function(f) f.ID = p.FUNCTION_ID)
                         From user In Context.SE_USER.Where(Function(f) f.ID = p.USER_ID)
                         Where user.USERNAME.ToUpper = Username.ToUpper
                         Select New PermissionDTO With {.ID = p.ID,
                                                        .FunctionID = p.FUNCTION_ID,
                                                        .GroupID = 0,
                                                        .FID = func.FID,
                                                        .MID = func.SE_MODULE.MID,
                                                        .AllowCreate = p.ALLOW_CREATE,
                                                        .AllowModify = p.ALLOW_MODIFY,
                                                        .AllowDelete = p.ALLOW_DELETE,
                                                        .AllowImport = p.ALLOW_IMPORT,
                                                        .AllowExport = p.ALLOW_EXPORT,
                                                        .AllowPrint = p.ALLOW_PRINT,
                                                        .AllowSpecial1 = p.ALLOW_SPECIAL1,
                                                        .AllowSpecial2 = p.ALLOW_SPECIAL2,
                                                        .AllowSpecial3 = p.ALLOW_SPECIAL3,
                                                        .AllowSpecial4 = p.ALLOW_SPECIAL4,
                                                        .AllowSpecial5 = p.ALLOW_SPECIAL5,
                                                        .IS_REPORT = False}

            Dim query3 = From p In Context.SE_REPORT
                         From n In p.SE_USER
                         Where n.USERNAME.ToUpper = Username.ToUpper
                         Select New PermissionDTO With {.ID = p.ID,
                                                        .FunctionID = p.ID,
                                                        .GroupID = 0,
                                                        .FID = p.CODE,
                                                        .MID = p.SE_MODULE.MID,
                                                        .AllowCreate = False,
                                                        .AllowModify = False,
                                                        .AllowDelete = False,
                                                        .AllowImport = False,
                                                        .AllowExport = False,
                                                        .AllowPrint = False,
                                                        .AllowSpecial1 = False,
                                                        .AllowSpecial2 = False,
                                                        .AllowSpecial3 = False,
                                                        .AllowSpecial4 = False,
                                                        .AllowSpecial5 = False,
                                                        .IS_REPORT = True}


            Dim lstPermissions As List(Of PermissionDTO) = query2.Union(query3).ToList

            Return lstPermissions
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CheckUserAdmin(ByVal Username As String) As Boolean
        Try
            If Username = "" Then Return False
            Dim u = (From p In Context.SE_USER
                        Where p.USERNAME.ToUpper = Username.ToUpper).FirstOrDefault

            If u Is Nothing Then Return False

            If u.MODULE_ADMIN = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUserWithPermision(ByVal Username As String) As UserDTO
        Try
            If Username = "" Then Return New UserDTO
            Dim query = (From p In Context.SE_USER
                         Where p.USERNAME.ToUpper = Username.ToUpper)
            Dim objUser = query.FirstOrDefault
            If objUser IsNot Nothing Then
                Dim isUserPermission As Boolean = True
                If objUser.MODULE_ADMIN Is Nothing Then
                    If (From p In Context.SE_USER_ORG_ACCESS
                        Where p.USER_ID = objUser.ID).Count = 0 Then
                        isUserPermission = False
                    End If

                    If (From p In Context.SE_USER_PERMISSION
                        Where p.USER_ID = objUser.ID).Count = 0 Then
                        isUserPermission = False
                    Else
                        isUserPermission = True
                    End If
                End If

                Return New UserDTO With {.ID = objUser.ID,
                                         .EMAIL = objUser.EMAIL,
                                         .TELEPHONE = objUser.TELEPHONE,
                                         .USERNAME = objUser.USERNAME,
                                         .FULLNAME = objUser.FULLNAME,
                                         .PASSWORD = objUser.PASSWORD,
                                         .IS_APP = objUser.IS_APP,
                                         .IS_PORTAL = objUser.IS_PORTAL,
                                         .IS_AD = objUser.IS_AD,
                                         .EMPLOYEE_CODE = objUser.EMPLOYEE_CODE,
                                         .ACTFLG = objUser.ACTFLG,
                                         .IS_CHANGE_PASS = objUser.IS_CHANGE_PASS,
                                         .MODULE_ADMIN = objUser.MODULE_ADMIN,
                                         .EMPLOYEE_ID = (From p In Context.HU_EMPLOYEE
                                                         Where p.EMPLOYEE_CODE = .EMPLOYEE_CODE
                                                         Order By p.ID Descending
                                                         Select p.ID).FirstOrDefault,
                                         .EFFECT_DATE = objUser.EFFECT_DATE,
                                         .EXPIRE_DATE = objUser.EXPIRE_DATE,
                                         .IS_USER_PERMISSION = isUserPermission}
            End If
            Return Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ChangeUserPassword(ByVal Username As String, ByVal _oldpass As String, ByVal _newpass As String, ByVal log As UserLog) As Boolean
        Try
            Dim query As SE_USER
            query = (From p In Context.SE_USER
                     Where p.USERNAME.ToUpper = Username.ToUpper
                     Select p).FirstOrDefault
            If query IsNot Nothing Then
                Using EncryptData As New EncryptData
                    If EncryptData.DecryptString(query.PASSWORD) = _oldpass Then
                        query.PASSWORD = EncryptData.EncryptString(_newpass)
                        query.IS_CHANGE_PASS = -1
                        query.CHANGE_PASS_DATE = Now.Date
                        Context.SaveChanges(log)
                    Else
                        Return False
                    End If

                End Using
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPassword(ByVal Username As String) As String
        Try
            Dim query As SE_USER
            query = (From p In Context.SE_USER
                     Where p.USERNAME.ToUpper = Username.ToUpper
                     Select p).FirstOrDefault
            Return query.PASSWORD
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateUserStatus(ByVal Username As String, ByVal _ACTFLG As String, ByVal log As UserLog) As Boolean
        Try
            Dim query As SE_USER
            query = (From p In Context.SE_USER
                     Where p.USERNAME.ToUpper = Username.ToUpper
                     Select p).FirstOrDefault

            If query IsNot Nothing Then
                query.ACTFLG = _ACTFLG
                Context.SaveChanges(log)
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Organization"

    Public Function GetOrganizationList() As List(Of OrganizationDTO)
        Dim lstOrgs As New List(Of OrganizationDTO)
        lstOrgs = (From o In Context.HU_ORGANIZATION
                   Order By o.NAME_VN
                   Select New OrganizationDTO With {
                      .ID = o.ID,
                      .DESCRIPTION_PATH = o.DESCRIPTION_PATH}).ToList
        Return lstOrgs
    End Function

    Public Function GetOrganizationAll() As List(Of OrganizationDTO)
        Dim lstOrgs As New List(Of OrganizationDTO)
        lstOrgs = (From p In Context.HU_ORGANIZATION
                   Order By p.ORD_NO, p.CODE, p.NAME_VN
                   Select New OrganizationDTO With {
                       .ID = p.ID,
                       .CODE = p.CODE,
                       .NAME_VN = p.NAME_VN,
                       .NAME_EN = p.NAME_EN,
                       .PARENT_ID = p.PARENT_ID,
                       .PARENT_NAME = p.PARENT.NAME_VN,
                       .ORD_NO = p.ORD_NO,
                       .ACTFLG = p.ACTFLG,
                       .DISSOLVE_DATE = p.DISSOLVE_DATE,
                       .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                       .FOUNDATION_DATE = p.FOUNDATION_DATE}).ToList
        Return lstOrgs
    End Function
    ' so do to chuc Location da duoc phan quyen
    Public Function GetOrganizationLocationTreeView(ByVal _username As String) As List(Of OrganizationDTO)
        Dim lstOrgs As New List(Of OrganizationDTO)
        Dim u As SE_USER = (From p In Context.SE_USER Where p.USERNAME.ToUpper = _username.ToUpper).FirstOrDefault
        If u IsNot Nothing Then
            Dim query1 = (From p In u.SE_GROUPS Where CBool(p.IS_ADMIN) = True Select p.ID).FirstOrDefault
            If query1 = Nothing Then
                Dim lstGroupIds As List(Of Decimal) = (From p In u.SE_GROUPS Select p.ID).ToList

                Dim query = (From org In Context.HU_ORGANIZATION
                           Where (From user In Context.SE_USER_ORG_ACCESS
                                  Where user.USER_ID = u.ID
                                  Select user.ORG_ID).Contains(org.ID)
                            Select New OrganizationDTO With {
                                .ID = org.ID,
                                .CODE = org.CODE,
                                .NAME_VN = org.NAME_VN,
                                .NAME_EN = org.NAME_EN,
                                .PARENT_ID = org.PARENT_ID,
                                .PARENT_NAME = org.PARENT.NAME_VN,
                                .ORD_NO = org.ORD_NO,
                                .ACTFLG = org.ACTFLG,
                                .DISSOLVE_DATE = org.DISSOLVE_DATE,
                                .HIERARCHICAL_PATH = org.HIERARCHICAL_PATH,
                                .DESCRIPTION_PATH = org.DESCRIPTION_PATH,
                                .FOUNDATION_DATE = org.FOUNDATION_DATE}).Distinct

                lstOrgs = query.ToList

                If lstOrgs.Count > 0 AndAlso (From p In lstOrgs Where p.HIERARCHICAL_PATH = "1").Count = 0 Then
                    Dim lstOrgPer = lstOrgs.Select(Function(f) f.ID).ToList
                    Dim lstOrgID As New List(Of Decimal)
                    For Each org In lstOrgs
                        If org.HIERARCHICAL_PATH <> "" Then
                            If org.HIERARCHICAL_PATH.Split(";").Length > 1 Then
                                For i As Integer = 0 To org.HIERARCHICAL_PATH.Split(";").Length - 2
                                    Dim str = org.HIERARCHICAL_PATH.Split(";")(i)
                                    If str <> "" Then
                                        Dim orgid = Decimal.Parse(str)
                                        If Not lstOrgPer.Contains(orgid) And Not lstOrgID.Contains(orgid) Then
                                            lstOrgID.Add(orgid)
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If lstOrgID.Count > 0 Then
                        Dim lstOrgNotPer = (From p In Context.HU_ORGANIZATION
                                            Where lstOrgID.Contains(p.ID)
                                            Select New OrganizationDTO With {
                                                .ID = p.ID,
                                                .CODE = p.CODE,
                                                .NAME_VN = p.NAME_VN,
                                                .NAME_EN = p.NAME_EN,
                                                .PARENT_ID = p.PARENT_ID,
                                                .PARENT_NAME = p.PARENT.NAME_VN,
                                                .ORD_NO = p.ORD_NO,
                                                .ACTFLG = p.ACTFLG,
                                                .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                                                .IS_NOT_PER = True}).ToList

                        lstOrgs.AddRange(lstOrgNotPer)
                    End If

                End If

                lstOrgs = (From p In lstOrgs Order By p.ORD_NO, p.CODE, p.NAME_VN).ToList

            Else
                lstOrgs = (From p In Context.HU_ORGANIZATION
                            Order By p.ORD_NO, p.CODE, p.NAME_VN
                            Select New OrganizationDTO With {
                                .ID = p.ID,
                                .CODE = p.CODE,
                                .NAME_VN = p.NAME_VN,
                                .NAME_EN = p.NAME_EN,
                                .PARENT_ID = p.PARENT_ID,
                                .PARENT_NAME = p.PARENT.NAME_VN,
                                .ORD_NO = p.ORD_NO,
                                .ACTFLG = p.ACTFLG,
                                .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                                .FOUNDATION_DATE = p.FOUNDATION_DATE}).ToList
            End If
        End If
        Return lstOrgs
    End Function

    Public Function GetOrganizationLocationInfo(ByVal _orgId As Decimal) As OrganizationDTO
        Dim query As OrganizationDTO
        query = (From p In Context.HU_ORGANIZATION Where p.ID = _orgId
                 Select New OrganizationDTO With {
                     .ACTFLG = p.ACTFLG,
                     .CODE = p.CODE,
                     .DISSOLVE_DATE = p.DISSOLVE_DATE,
                     .FOUNDATION_DATE = p.FOUNDATION_DATE,
                     .ID = p.ID,
                     .NAME_EN = p.NAME_EN,
                     .NAME_VN = p.NAME_VN,
                     .PARENT_ID = p.PARENT_ID,
                     .PARENT_NAME = p.PARENT.NAME_VN,
                     .REMARK = p.REMARK}).FirstOrDefault
        Return query
    End Function

    Public Function GetOrganizationStructureInfo(ByVal _orgId As Decimal) As List(Of OrganizationStructureDTO)
        Dim query As New OrganizationStructureDTO
        Dim list As New List(Of OrganizationStructureDTO)
        query.PARENT_ID = _orgId

        Do While query.PARENT_ID IsNot Nothing
            query = (From p In Context.HU_ORGANIZATION Where p.ID = query.PARENT_ID
                 Select New OrganizationStructureDTO With {
                     .ID = p.ID,
                     .CODE = p.CODE,
                     .NAME_VN = p.NAME_VN,
                     .NAME_EN = p.NAME_EN,
                     .PARENT_ID = p.PARENT_ID}).FirstOrDefault
            list.Add(query)
        Loop
        Return list
    End Function
    Public Function GetListOrgImport(ByVal orgID As List(Of Decimal)) As List(Of OrganizationDTO)
        Dim result = (From p In Context.HU_ORGANIZATION
                      Where orgID.Contains(p.ID)
                      Select New OrganizationDTO With {.ID = p.ID, .NAME_VN = p.NAME_VN}).ToList()
        Return result
    End Function
#End Region

#Region "Employee"
    Public Function GetEmployeeSignToPopupFind(_filter As EmployeePopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        Try
            Dim userName As String
            Dim common As String = "Dùng chung"
            Using cls As New DataAccess.QueryData
                userName = log.Username
                If _filter.LoadAllOrganization Then
                    userName = "ADMIN"
                End If
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = userName,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From sign In Context.HU_SIGNER.Where(Function(f) f.ACTFLG = 1)
                        From p In Context.HU_EMPLOYEE.Where(Function(f) f.EMPLOYEE_CODE = sign.SIGNER_CODE).DefaultIfEmpty
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = sign.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From work_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_STATUS And f.TYPE_ID = 59).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = userName)
                        From te In Context.HU_TERMINATE.Where(Function(f) p.ID = f.EMPLOYEE_ID).DefaultIfEmpty

            If _filter.MustHaveContract Then
                query = query.Where(Function(f) f.p.JOIN_DATE.HasValue)
            End If
            Dim dateNow As Date? = Date.Now.Date

            If _filter.IS_TER Then
                query = query.Where(Function(f) f.p.WORK_STATUS = -1 And f.p.TER_EFFECT_DATE IsNot Nothing)
            Else
                query = query.Where(Function(f) f.p.WORK_STATUS Is Nothing Or (f.p.WORK_STATUS IsNot Nothing And _
                                         (f.p.WORK_STATUS <> -1 Or (f.p.WORK_STATUS = -1 And f.p.TER_EFFECT_DATE > dateNow))))
            End If

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or _
                                        f.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            Dim lst = query.Select(Function(f) New EmployeePopupFindListDTO With {
                            .EMPLOYEE_CODE = f.sign.SIGNER_CODE,
                            .ID = f.p.ID,
                            .EMPLOYEE_ID = f.p.ID,
                            .FULLNAME_VN = f.p.FULLNAME_VN,
                            .JOIN_DATE = f.p.JOIN_DATE,
                            .ORG_NAME = If(f.sign.ORG_ID = -1, common, f.o.NAME_VN),
                            .ORG_DESC = f.o.DESCRIPTION_PATH,
                            .GENDER = f.gender.NAME_VN,
                            .TITLE_NAME = f.t.NAME_VN})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeToPopupFind(_filter As EmployeePopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        Try
            Dim userName As String
            Using cls As New DataAccess.QueryData
                userName = log.Username.ToUpper
                If _filter.LoadAllOrganization Then
                    userName = "ADMIN"
                End If
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = userName,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_EMPLOYEE
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From work_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_STATUS And f.TYPE_ID = 59).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = userName)
                        From te In Context.HU_TERMINATE.Where(Function(f) p.ID = f.EMPLOYEE_ID).DefaultIfEmpty
            'From te_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = te.STATUS_ID And f.NAME_VN.ToUpper.Trim <> "PHÊ DUYỆT")

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or _
                                        f.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.DIRECT_MANAGER IsNot Nothing Then
                query = query.Where(Function(f) f.p.DIRECT_MANAGER.ToString().Contains(_filter.DIRECT_MANAGER) Or _
                                        f.p.FULLNAME_VN.ToUpper.Contains(_filter.DIRECT_MANAGER))
            End If

            If _filter.MustHaveContract Then
                query = query.Where(Function(f) f.p.CONTRACT_ID.HasValue)
            End If
            Dim dateNow = Date.Now.Date
            If Not _filter.IsOnlyWorkingWithoutTer Then
                If _filter.IS_TER Then
                    query = query.Where(Function(f) f.p.WORK_STATUS = 257 And f.p.TER_EFFECT_DATE <= dateNow)
                Else
                    query = query.Where(Function(f) f.p.WORK_STATUS Is Nothing Or (f.p.WORK_STATUS IsNot Nothing And _
                                             (f.p.WORK_STATUS <> 257 Or (f.p.WORK_STATUS = 257 And f.te.LAST_DATE > dateNow))))
                End If
            Else
                query = query.Where(Function(f) f.p.WORK_STATUS Is Nothing Or (f.p.WORK_STATUS IsNot Nothing And f.p.WORK_STATUS <> 257))
            End If
            Select Case _filter.IS_3B
                Case 1
                    query = query.Where(Function(f) f.p.IS_3B = True)
                Case 2
                    query = query.Where(Function(f) f.p.IS_3B = False)

            End Select

            Dim lst = query.Select(Function(f) New EmployeePopupFindListDTO With {
                            .EMPLOYEE_CODE = f.p.EMPLOYEE_CODE,
                            .ID = f.p.ID,
                            .EMPLOYEE_ID = f.p.ID,
                            .FULLNAME_VN = f.p.FULLNAME_VN,
                            .FULLNAME_EN = f.p.FULLNAME_EN,
                            .JOIN_DATE = f.p.JOIN_DATE,
                            .ORG_NAME = f.o.NAME_VN,
                            .ORG_DESC = f.o.DESCRIPTION_PATH,
                            .GENDER = f.gender.NAME_VN,
                            .WORK_STATUS = f.work_status.NAME_VN,
                            .TITLE_NAME = f.t.NAME_VN,
                            .IMAGE = f.cv.IMAGE,
                            .DIRECT_MANAGER = f.p.DIRECT_MANAGER}) '.Where(Function(x) x.WORK_STATUS = "Đang làm việc")

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeToPopupFind2(ByVal _filter As EmployeePopupFindListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal request_id As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        Try
            Dim query = From p In Context.HU_EMPLOYEE
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From work_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_STATUS And f.TYPE_ID = 59).DefaultIfEmpty
            'xemlai...
            'From k In Context.ORG_TEMP_TABLE.Where(Function(f) p.ORG_ID = f.ORG_ID And f.REQUEST_ID = request_id)

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or _
                                        f.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.MustHaveContract Then
                query = query.Where(Function(f) f.p.CONTRACT_ID.HasValue)
            End If
            Dim dateNow = Date.Now.Date
            If Not _filter.IsOnlyWorkingWithoutTer Then
                If _filter.IS_TER Then
                    query = query.Where(Function(f) f.p.WORK_STATUS = 257 And f.p.TER_EFFECT_DATE <= dateNow)
                Else
                    query = query.Where(Function(f) f.p.WORK_STATUS Is Nothing Or (f.p.WORK_STATUS IsNot Nothing And _
                                             (f.p.WORK_STATUS <> 257 Or (f.p.WORK_STATUS = 257 And f.p.TER_EFFECT_DATE > dateNow))))
                End If
            Else
                query = query.Where(Function(f) f.p.WORK_STATUS Is Nothing Or (f.p.WORK_STATUS IsNot Nothing And f.p.WORK_STATUS <> 257))
            End If
            Select Case _filter.IS_3B
                Case 1
                    query = query.Where(Function(f) f.p.IS_3B = True)
                Case 2
                    query = query.Where(Function(f) f.p.IS_3B = False)

            End Select

            Dim lst = query.Select(Function(f) New EmployeePopupFindListDTO With {
                            .EMPLOYEE_CODE = f.p.EMPLOYEE_CODE,
                            .ID = f.p.ID,
                            .EMPLOYEE_ID = f.p.ID,
                            .FULLNAME_VN = f.p.FULLNAME_VN,
                            .FULLNAME_EN = f.p.FULLNAME_EN,
                            .JOIN_DATE = f.p.JOIN_DATE,
                            .ORG_NAME = f.o.NAME_VN,
                            .ORG_DESC = f.o.DESCRIPTION_PATH,
                            .GENDER = f.gender.NAME_VN,
                            .WORK_STATUS = f.work_status.NAME_VN,
                            .TITLE_NAME = f.t.NAME_VN})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeToPopupFind_EmployeeID(ByVal _empId As List(Of Decimal)) As List(Of EmployeePopupFindDTO)
        Dim result = (From p In Context.HU_EMPLOYEE
                      From cv In Context.HU_EMPLOYEE_CV.Where(Function(cv) cv.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                      From s In Context.HU_STAFF_RANK.Where(Function(s) s.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                      Order By p.EMPLOYEE_CODE
                      Where _empId.Contains(p.ID)
                      Select New EmployeePopupFindDTO With {
                          .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                          .ID = p.ID,
                          .EMPLOYEE_ID = p.ID,
                          .JOIN_DATE = p.JOIN_DATE,
                          .FULLNAME_VN = p.FULLNAME_VN,
                          .ORG_ID = p.ORG_ID,
                          .ORG_NAME = p.HU_ORGANIZATION.NAME_VN,
                          .ORG_DESC = p.HU_ORGANIZATION.DESCRIPTION_PATH,
                          .TITLE_ID = p.TITLE_ID,
                          .TITLE_NAME = p.HU_TITLE.NAME_VN,
                          .BIRTH_DATE = cv.BIRTH_DATE,
                          .BIRTH_PLACE = cv.BIRTH_PLACE,
                          .STAFF_RANK_ID = p.STAFF_RANK_ID,
                          .STAFF_RANK_NAME = s.NAME,
                          .IMAGE = cv.IMAGE})

        Return result.ToList
    End Function

    Public Function GetEmployeeID(ByVal _empId As Decimal) As EmployeePopupFindDTO
        Dim result = (From p In Context.HU_EMPLOYEE
                      From cv In Context.HU_EMPLOYEE_CV.Where(Function(cv) cv.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                      From s In Context.HU_STAFF_RANK.Where(Function(s) s.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                      Order By p.EMPLOYEE_CODE
                      Where p.ID = _empId
                      Select New EmployeePopupFindDTO With {
                          .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                          .ID = p.ID,
                          .EMPLOYEE_ID = p.ID,
                          .JOIN_DATE = p.JOIN_DATE,
                          .FULLNAME_VN = p.FULLNAME_VN,
                          .ORG_ID = p.ORG_ID,
                          .ORG_NAME = p.HU_ORGANIZATION.NAME_VN,
                          .ORG_DESC = p.HU_ORGANIZATION.DESCRIPTION_PATH,
                          .TITLE_ID = p.TITLE_ID,
                          .TITLE_NAME = p.HU_TITLE.NAME_VN,
                          .BIRTH_DATE = cv.BIRTH_DATE,
                          .BIRTH_PLACE = cv.BIRTH_PLACE,
                          .STAFF_RANK_ID = p.STAFF_RANK_ID,
                          .STAFF_RANK_NAME = s.NAME}).SingleOrDefault

        Return result
    End Function
#End Region

#Region "Title"

    Public Function GetTitle(ByVal _filter As TitleDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)

        Try
            Dim query = From p In Context.HU_TITLE
                        From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                        From orgLv In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.ACTFLG = "A"

            Dim lst = query.Select(Function(p) New TitleDTO With {
                                  .ID = p.p.ID,
                                  .CODE = p.p.CODE,
                                  .NAME_EN = p.p.NAME_EN,
                                  .NAME_VN = p.p.NAME_VN,
                                  .REMARK = p.p.REMARK,
                                  .TITLE_GROUP_ID = p.p.TITLE_GROUP_ID,
                                  .TITLE_GROUP_NAME = p.group.NAME_VN,
                                  .CREATED_DATE = p.p.CREATED_DATE,
                                  .ACTFLG = p.p.ACTFLG,
                                  .ORG_ID_NAME = p.orgLv.NAME_VN})

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.TITLE_GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTitleFromId(ByVal _lstIds As List(Of Decimal)) As List(Of TitleDTO)
        Try
            Dim query = (From p In Context.HU_TITLE Where _lstIds.Contains(p.ID)
                        Select New TitleDTO With {
                                  .ID = p.ID,
                                  .CODE = p.CODE,
                                  .NAME_EN = p.NAME_EN,
                                  .NAME_VN = p.NAME_VN,
                                  .ACTFLG = p.ACTFLG}).ToList
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "System Configurate"

    Public Function GetConfig(ByVal eModule As ModuleID) As Dictionary(Of String, String)
        Using cofig As New SystemConfig
            Try
                Return cofig.GetConfig(eModule)
            Catch ex As Exception
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateConfig(ByVal _lstConfig As Dictionary(Of String, String), ByVal eModule As ModuleID) As Boolean
        Using cofig As New SystemConfig
            Try
                Return cofig.UpdateConfig(_lstConfig, eModule)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Reminder per user"

    Public Function GetReminderConfig(ByVal username As String) As Dictionary(Of Integer, String)
        Using cofig As New SystemConfig
            Try
                Return cofig.GetReminderConfig(username)
            Catch ex As Exception
                Throw
            End Try
        End Using

    End Function

    Public Function SetReminderConfig(ByVal username As String, ByVal type As Integer, ByVal value As String) As Boolean
        Using cofig As New SystemConfig
            Try
                Return cofig.SetReminderConfig(username, type, value)
            Catch ex As Exception
                Throw
            End Try
        End Using
    End Function

#End Region


End Class
