Imports System.IO
Imports Framework.Data
Imports System.Data.Objects
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.DataAccess
Imports Oracle.DataAccess.Client
Imports System.Reflection

Partial Class ProfileRepository
#Region "HU_CERTIFICATE_edit"
    Public Function GetCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Dim query As ObjectQuery(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Try
            query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                    From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID).DefaultIfEmpty
                    From OT1 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE).DefaultIfEmpty
                    From OT2 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.LEVEL_ID).DefaultIfEmpty
                     Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                         .ID = p.ID,
                        .EMPLOYEE_ID = p.EMPLOYEE_ID,
                        .FROM_DATE = p.FROM_DATE,
                        .TO_DATE = p.TO_DATE,
                         .RECEIVE_DEGREE_DATE = p.RECEIVE_DEGREE_DATE,
                        .YEAR_GRA = p.YEAR_GRA,
                        .NAME_SHOOLS = p.NAME_SHOOLS,
                        .FORM_TRAIN_ID = p.FORM_TRAIN_ID,
                        .FORM_TRAIN_NAME = ot.NAME_VN,
                         .LEVEL_ID = p.LEVEL_ID,
                         .LEVEL_NAME = OT2.NAME_VN,
                         .SCORE = p.SCORE,
                         .CONTENT_TRAIN = p.CONTENT_TRAIN,
                         .REMARK = p.REMARK,
                         .CODE_CERTIFICATE = p.CODE_CERTIFICATE,
                        .SPECIALIZED_TRAIN = p.SPECIALIZED_TRAIN,
                        .RESULT_TRAIN = p.RESULT_TRAIN,
                        .CERTIFICATE = OT1.NAME_VN,
                        .CERTIFICATE_ID = p.CERTIFICATE,
                        .EFFECTIVE_DATE_FROM = p.EFFECTIVE_DATE_FROM,
                        .EFFECTIVE_DATE_TO = p.EFFECTIVE_DATE_TO,
                        .CREATED_BY = p.CREATED_BY,
                         .IS_RENEWED = p.IS_RENEW,
                         .TYPE_TRAIN_ID = p.TYPE_TRAIN_ID,
                         .TYPE_TRAIN_NAME = p.TYPE_TRAIN_NAME,
                        .CREATED_DATE = p.CREATED_DATE,
                        .CREATED_LOG = p.CREATED_LOG,
                        .MODIFIED_BY = p.MODIFIED_BY,
                        .MODIFIED_DATE = p.MODIFIED_DATE,
                        .MODIFIED_LOG = p.MODIFIED_LOG,
                        .REASON_UNAPROVE = p.REASON_UNAPROVE,
                        .FK_PKEY = p.FK_PKEY,
                         .UPLOAD_FILE = p.UPLOAD_FILE,
                         .FILE_NAME = p.FILE_NAME,
                        .STATUS = p.STATUS,
                        .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                           If(p.STATUS = 1, "Chờ phê duyệt",
                                              If(p.STATUS = 2, "Phê duyệt",
                                                 If(p.STATUS = 3, "Không phê duyệt", ""))))})

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.STATUS <> "" Then
                query = query.Where(Function(p) p.STATUS = _filter.STATUS)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If
            Return query.ToList
        Catch ex As Exception

        End Try
    End Function
    Public Function InsertCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                             ByVal log As UserLog,
                                             ByRef gID As Decimal) As Boolean
        Try

            Dim objCertificatetData As New HU_PRO_TRAIN_OUT_COMPANY_EDIT
            objCertificatetData.ID = Utilities.GetNextSequence(Context, Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT.EntitySet.Name)
            objCertificatetData.FROM_DATE = objCertificateEdit.FROM_DATE
            objCertificatetData.TO_DATE = objCertificateEdit.TO_DATE
            objCertificatetData.YEAR_GRA = objCertificateEdit.YEAR_GRA
            objCertificatetData.NAME_SHOOLS = objCertificateEdit.NAME_SHOOLS
            objCertificatetData.UPLOAD_FILE = objCertificateEdit.UPLOAD_FILE
            objCertificatetData.FILE_NAME = objCertificateEdit.FILE_NAME
            objCertificatetData.FORM_TRAIN_ID = objCertificateEdit.FORM_TRAIN_ID
            objCertificatetData.SPECIALIZED_TRAIN = objCertificateEdit.SPECIALIZED_TRAIN
            objCertificatetData.RESULT_TRAIN = objCertificateEdit.RESULT_TRAIN
            objCertificatetData.CERTIFICATE = objCertificateEdit.CERTIFICATE
            objCertificatetData.EFFECTIVE_DATE_FROM = objCertificateEdit.EFFECTIVE_DATE_FROM
            objCertificatetData.EFFECTIVE_DATE_TO = objCertificateEdit.EFFECTIVE_DATE_TO
            objCertificatetData.EMPLOYEE_ID = objCertificateEdit.EMPLOYEE_ID
            objCertificatetData.SCORE = objCertificateEdit.SCORE
            objCertificatetData.CONTENT_TRAIN = objCertificateEdit.CONTENT_TRAIN
            objCertificatetData.TYPE_TRAIN_NAME = objCertificateEdit.TYPE_TRAIN_NAME
            objCertificatetData.CODE_CERTIFICATE = objCertificateEdit.CODE_CERTIFICATE
            objCertificatetData.REMARK = objCertificateEdit.REMARK
            objCertificatetData.LEVEL_ID = objCertificateEdit.LEVEL_ID
            objCertificatetData.TYPE_TRAIN_ID = objCertificateEdit.TYPE_TRAIN_ID
            objCertificatetData.FK_PKEY = objCertificateEdit.FK_PKEY
            objCertificatetData.STATUS = 0
            objCertificatetData.RECEIVE_DEGREE_DATE = objCertificateEdit.RECEIVE_DEGREE_DATE
            objCertificatetData.IS_RENEW = objCertificateEdit.IS_RENEWED
            Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT.AddObject(objCertificatetData)
            Context.SaveChanges(log)
            gID = objCertificatetData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ModifyCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                             ByVal log As UserLog,
                                             ByRef gID As Decimal) As Boolean
        Dim objCertificatetData As New HU_PRO_TRAIN_OUT_COMPANY_EDIT With {.ID = objCertificateEdit.ID}
        Try
            objCertificatetData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where p.ID = objCertificateEdit.ID).FirstOrDefault
            objCertificatetData.ID = objCertificateEdit.ID
            objCertificatetData.FROM_DATE = objCertificateEdit.FROM_DATE
            objCertificatetData.TO_DATE = objCertificateEdit.TO_DATE
            objCertificatetData.UPLOAD_FILE = objCertificateEdit.UPLOAD_FILE
            objCertificatetData.FILE_NAME = objCertificateEdit.FILE_NAME
            objCertificatetData.YEAR_GRA = objCertificateEdit.YEAR_GRA
            objCertificatetData.NAME_SHOOLS = objCertificateEdit.NAME_SHOOLS
            objCertificatetData.FORM_TRAIN_ID = objCertificateEdit.FORM_TRAIN_ID
            objCertificatetData.SPECIALIZED_TRAIN = objCertificateEdit.SPECIALIZED_TRAIN
            objCertificatetData.RESULT_TRAIN = objCertificateEdit.RESULT_TRAIN
            objCertificatetData.CERTIFICATE = objCertificateEdit.CERTIFICATE
            objCertificatetData.EFFECTIVE_DATE_FROM = objCertificateEdit.EFFECTIVE_DATE_FROM
            objCertificatetData.EFFECTIVE_DATE_TO = objCertificateEdit.EFFECTIVE_DATE_TO
            objCertificatetData.EMPLOYEE_ID = objCertificateEdit.EMPLOYEE_ID
            objCertificatetData.SCORE = objCertificateEdit.SCORE
            objCertificatetData.CONTENT_TRAIN = objCertificateEdit.CONTENT_TRAIN
            objCertificatetData.TYPE_TRAIN_NAME = objCertificateEdit.TYPE_TRAIN_NAME
            objCertificatetData.CODE_CERTIFICATE = objCertificateEdit.CODE_CERTIFICATE
            objCertificatetData.LEVEL_ID = objCertificateEdit.LEVEL_ID
            objCertificatetData.REMARK = objCertificateEdit.REMARK
            objCertificatetData.TYPE_TRAIN_ID = objCertificateEdit.TYPE_TRAIN_ID
            objCertificatetData.FK_PKEY = objCertificateEdit.FK_PKEY
            objCertificatetData.STATUS = 0
            objCertificatetData.RECEIVE_DEGREE_DATE = objCertificateEdit.RECEIVE_DEGREE_DATE
            objCertificatetData.IS_RENEW = objCertificateEdit.IS_RENEWED
            Context.SaveChanges(log)
            gID = objCertificatetData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function CheckExistCertificateEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT
        Try
            Dim query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                         Where p.STATUS <> 2 And p.FK_PKEY = pk_key
                         Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                             .ID = p.ID,
                             .STATUS = p.STATUS}).FirstOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region
#Region "HU_CERTIFICATE"
    Public Function SendCertificateEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean
        Try
            Dim lstObj = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.STATUS = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    'get dl bang hu_certificate
    Public Function GetCertificate(ByVal _filter As CETIFICATEDTO) As List(Of CETIFICATEDTO)
        Dim query As ObjectQuery(Of CETIFICATEDTO)
        Try
            query = (From p In Context.HU_CERTIFICATE
                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                     From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.FIELD_TRAIN).DefaultIfEmpty
                     From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MAJOR).DefaultIfEmpty
                     From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEVEL_TRAIN).DefaultIfEmpty
                     Select New CETIFICATEDTO With {
                         .ID = p.ID,
                         .FIELD = p.FIELD_TRAIN,
                         .FIELD_NAME = ot.NAME_VN,
                         .FROM_DATE = p.FROM_DATE,
                         .TO_DATE = p.TO_DATE,
                         .SCHOOL_NAME = p.SCHOOL_NAME,
                         .MAJOR = p.MAJOR,
                         .MAJOR_NAME = ot1.NAME_VN,
                         .LEVEL = p.LEVEL_TRAIN,
                         .LEVEL_NAME = ot2.NAME_VN,
                         .MARK = p.MARK,
                         .CONTENT_NAME = p.CONTENT_TRAIN,
                         .TYPE_NAME = p.TYPE_TRAIN,
                         .CODE_CERTIFICATE = p.CODE_CETIFICATE,
                         .EFFECT_FROM = p.EFFECT_FROM,
                         .EFFECT_TO = p.EFFECT_TO,
                         .CLASSIFICATION = p.CLASSIFICATION,
                         .YEAR = p.YEAR,
                         .REMARK = p.REMARK,
                         .RENEW = p.RENEW,
                         .UPLOAD = p.UPLOAD_FILE,
                         .FILENAME = p.FILE_NAME
                         })
            Return query.ToList
        Catch ex As Exception

        End Try
    End Function
#End Region
#Region "Family"

    Public Function InsertEmployeeFamily(ByVal objFamily As FamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objFamilyData As New HU_FAMILY
            objFamilyData.ID = Utilities.GetNextSequence(Context, Context.HU_FAMILY.EntitySet.Name)
            objFamilyData.EMPLOYEE_ID = objFamily.EMPLOYEE_ID
            objFamilyData.FULLNAME = objFamily.FULLNAME
            objFamilyData.RELATION_ID = objFamily.RELATION_ID
            objFamilyData.PROVINCE_ID = objFamily.PROVINCE_ID
            objFamilyData.CAREER = objFamily.CAREER
            objFamilyData.TITLE_NAME = objFamily.TITLE_NAME
            objFamilyData.BIRTH_DATE = objFamily.BIRTH_DATE
            objFamilyData.DEDUCT_REG = objFamily.DEDUCT_REG
            objFamilyData.ID_NO = objFamily.ID_NO
            objFamilyData.TAXTATION = objFamily.TAXTATION
            objFamilyData.IS_DEDUCT = objFamily.IS_DEDUCT
            objFamilyData.DEDUCT_FROM = objFamily.DEDUCT_FROM
            objFamilyData.DEDUCT_TO = objFamily.DEDUCT_TO
            objFamilyData.REMARK = objFamily.REMARK
            objFamilyData.ADDRESS = objFamily.ADDRESS
            objFamilyData.ADDRESS_TT = objFamily.ADDRESS_TT
            objFamilyData.AD_DISTRICT_ID = objFamily.AD_DISTRICT_ID
            objFamilyData.AD_PROVINCE_ID = objFamily.AD_PROVINCE_ID
            objFamilyData.AD_VILLAGE = objFamily.AD_VILLAGE
            objFamilyData.AD_WARD_ID = objFamily.AD_WARD_ID
            objFamilyData.TT_DISTRICT_ID = objFamily.TT_DISTRICT_ID
            objFamilyData.TT_PROVINCE_ID = objFamily.TT_PROVINCE_ID
            objFamilyData.TT_WARD_ID = objFamily.TT_WARD_ID
            objFamilyData.IS_OWNER = objFamily.IS_OWNER
            objFamilyData.IS_PASS = objFamily.IS_PASS
            objFamilyData.CERTIFICATE_CODE = objFamily.CERTIFICATE_CODE
            objFamilyData.CERTIFICATE_NUM = objFamily.CERTIFICATE_NUM

            objFamilyData.NATION_ID = objFamily.NATION_ID
            objFamilyData.ID_NO_DATE = objFamily.ID_NO_DATE
            objFamilyData.ID_NO_PLACE_NAME = objFamily.ID_NO_PLACE_NAME
            objFamilyData.PHONE = objFamily.PHONE
            objFamilyData.TAXTATION_DATE = objFamily.TAXTATION_DATE
            objFamilyData.TAXTATION_PLACE = objFamily.TAXTATION_PLACE
            objFamilyData.BIRTH_CODE = objFamily.BIRTH_CODE
            objFamilyData.QUYEN = objFamily.QUYEN
            objFamilyData.BIRTH_NATION_ID = objFamily.BIRTH_NATION_ID
            objFamilyData.BIRTH_PROVINCE_ID = objFamily.BIRTH_PROVINCE_ID
            objFamilyData.BIRTH_DISTRICT_ID = objFamily.BIRTH_DISTRICT_ID
            objFamilyData.BIRTH_WARD_ID = objFamily.BIRTH_WARD_ID
            objFamilyData.GENDER = objFamily.GENDER

            Context.HU_FAMILY.AddObject(objFamilyData)
            Context.SaveChanges(log)
            gID = objFamilyData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyEmployeeFamily(ByVal objFamily As FamilyDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objFamilyData As New HU_FAMILY With {.ID = objFamily.ID}
        Try
            objFamilyData = (From p In Context.HU_FAMILY Where p.ID = objFamily.ID).FirstOrDefault
            objFamilyData.EMPLOYEE_ID = objFamily.EMPLOYEE_ID
            objFamilyData.FULLNAME = objFamily.FULLNAME
            objFamilyData.RELATION_ID = objFamily.RELATION_ID
            objFamilyData.PROVINCE_ID = objFamily.PROVINCE_ID
            objFamilyData.CAREER = objFamily.CAREER
            objFamilyData.TITLE_NAME = objFamily.TITLE_NAME
            objFamilyData.BIRTH_DATE = objFamily.BIRTH_DATE
            objFamilyData.DEDUCT_REG = objFamily.DEDUCT_REG
            objFamilyData.ID_NO = objFamily.ID_NO
            objFamilyData.TAXTATION = objFamily.TAXTATION
            objFamilyData.IS_DEDUCT = objFamily.IS_DEDUCT
            objFamilyData.DEDUCT_FROM = objFamily.DEDUCT_FROM
            objFamilyData.DEDUCT_TO = objFamily.DEDUCT_TO
            objFamilyData.REMARK = objFamily.REMARK
            objFamilyData.ADDRESS = objFamily.ADDRESS
            objFamilyData.ADDRESS_TT = objFamily.ADDRESS_TT
            objFamilyData.AD_DISTRICT_ID = objFamily.AD_DISTRICT_ID
            objFamilyData.AD_PROVINCE_ID = objFamily.AD_PROVINCE_ID
            objFamilyData.AD_VILLAGE = objFamily.AD_VILLAGE
            objFamilyData.AD_WARD_ID = objFamily.AD_WARD_ID
            objFamilyData.TT_DISTRICT_ID = objFamily.TT_DISTRICT_ID
            objFamilyData.TT_PROVINCE_ID = objFamily.TT_PROVINCE_ID
            objFamilyData.TT_WARD_ID = objFamily.TT_WARD_ID
            objFamilyData.IS_OWNER = objFamily.IS_OWNER
            objFamilyData.IS_PASS = objFamily.IS_PASS
            objFamilyData.CERTIFICATE_CODE = objFamily.CERTIFICATE_CODE
            objFamilyData.CERTIFICATE_NUM = objFamily.CERTIFICATE_NUM

            objFamilyData.NATION_ID = objFamily.NATION_ID
            objFamilyData.ID_NO_DATE = objFamily.ID_NO_DATE
            objFamilyData.ID_NO_PLACE_NAME = objFamily.ID_NO_PLACE_NAME
            objFamilyData.PHONE = objFamily.PHONE
            objFamilyData.TAXTATION_DATE = objFamily.TAXTATION_DATE
            objFamilyData.TAXTATION_PLACE = objFamily.TAXTATION_PLACE
            objFamilyData.BIRTH_CODE = objFamily.BIRTH_CODE
            objFamilyData.QUYEN = objFamily.QUYEN
            objFamilyData.BIRTH_NATION_ID = objFamily.BIRTH_NATION_ID
            objFamilyData.BIRTH_PROVINCE_ID = objFamily.BIRTH_PROVINCE_ID
            objFamilyData.BIRTH_DISTRICT_ID = objFamily.BIRTH_DISTRICT_ID
            objFamilyData.BIRTH_WARD_ID = objFamily.BIRTH_WARD_ID
            objFamilyData.GENDER = objFamily.GENDER

            Context.SaveChanges(log)
            gID = objFamilyData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function


    Public Function GetEmployeeFamily(ByVal _filter As FamilyDTO) As List(Of FamilyDTO)
        Dim query As ObjectQuery(Of FamilyDTO)
        Try
            query = (From p In Context.HU_FAMILY
                     Group Join m In Context.OT_OTHER_LIST On p.RELATION_ID Equals m.ID Into gGroup = Group
                     From p_g In gGroup.DefaultIfEmpty
                     Group Join n In Context.HU_PROVINCE On p.PROVINCE_ID Equals n.ID Into nGroup = Group
                     From n_g In nGroup.DefaultIfEmpty
                     Group Join pv In Context.HU_PROVINCE On p.AD_PROVINCE_ID Equals pv.ID Into pvGroup = Group
                     From pv_g In pvGroup.DefaultIfEmpty
                    Group Join d In Context.HU_DISTRICT On p.AD_DISTRICT_ID Equals d.ID Into dGroup = Group
                     From d_g In dGroup.DefaultIfEmpty
                    Group Join w In Context.HU_WARD On p.AD_WARD_ID Equals w.ID Into wGroup = Group
                     From w_g In wGroup.DefaultIfEmpty
                      Group Join pvT In Context.HU_PROVINCE On p.TT_PROVINCE_ID Equals pvT.ID Into pvTGroup = Group
                     From pvT_g In pvTGroup.DefaultIfEmpty
                    Group Join dT In Context.HU_DISTRICT On p.TT_DISTRICT_ID Equals dT.ID Into dTGroup = Group
                     From dT_g In dTGroup.DefaultIfEmpty
                    Group Join wT In Context.HU_WARD On p.TT_WARD_ID Equals wT.ID Into wTGroup = Group
                     From wT_g In wTGroup.DefaultIfEmpty
                     From n_tt In Context.HU_NATION.Where(Function(F) F.ID = p.NATION_ID).DefaultIfEmpty
                     From n_ks In Context.HU_NATION.Where(Function(F) F.ID = p.BIRTH_NATION_ID).DefaultIfEmpty
                     From p_ks In Context.HU_PROVINCE.Where(Function(F) F.ID = p.BIRTH_PROVINCE_ID).DefaultIfEmpty
                     From d_ks In Context.HU_DISTRICT.Where(Function(F) F.ID = p.BIRTH_DISTRICT_ID).DefaultIfEmpty
                     From w_ks In Context.HU_WARD.Where(Function(F) F.ID = p.BIRTH_WARD_ID).DefaultIfEmpty
                     From g In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.GENDER).DefaultIfEmpty
                   Select New FamilyDTO With {
                    .ID = p.ID,
                    .ADDRESS = p.ADDRESS,
                    .ADDRESS_TT = p.ADDRESS_TT,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .FULLNAME = p.FULLNAME,
                    .RELATION_ID = p.RELATION_ID,
                    .RELATION_NAME = p_g.NAME_VN,
                    .PROVINCE_ID = p.PROVINCE_ID,
                    .PROVINCE_NAME = n_g.NAME_VN,
                    .CAREER = p.CAREER,
                    .TITLE_NAME = p.TITLE_NAME,
                    .BIRTH_DATE = p.BIRTH_DATE,
                    .TAXTATION = p.TAXTATION,
                    .DEDUCT_REG = p.DEDUCT_REG,
                    .ID_NO = p.ID_NO,
                    .IS_DEDUCT = p.IS_DEDUCT,
                    .DEDUCT_FROM = p.DEDUCT_FROM,
                    .DEDUCT_TO = p.DEDUCT_TO,
                    .REMARK = p.REMARK,
                    .AD_DISTRICT_ID = p.AD_DISTRICT_ID,
                    .AD_DISTRICT_NAME = d_g.NAME_VN,
                    .AD_PROVINCE_ID = p.AD_PROVINCE_ID,
                    .AD_PROVINCE_NAME = pv_g.NAME_VN,
                    .AD_VILLAGE = p.AD_VILLAGE,
                    .AD_WARD_ID = p.AD_WARD_ID,
                    .AD_WARD_NAME = w_g.NAME_EN,
                    .TT_DISTRICT_ID = p.TT_DISTRICT_ID,
                     .TT_DISTRICT_NAME = dT_g.NAME_VN,
                    .TT_PROVINCE_ID = p.TT_PROVINCE_ID,
                    .TT_PROVINCE_NAME = pvT_g.NAME_VN,
                    .TT_WARD_ID = p.TT_WARD_ID,
                    .TT_WARD_NAME = wT_g.NAME_EN,
                    .IS_OWNER = p.IS_OWNER,
                    .IS_PASS = p.IS_PASS,
                    .CERTIFICATE_CODE = p.CERTIFICATE_CODE,
                    .CERTIFICATE_NUM = p.CERTIFICATE_NUM,
                    .NATION_ID = p.NATION_ID,
                    .NATION_NAME = n_tt.NAME_VN,
                    .ID_NO_DATE = p.ID_NO_DATE,
                    .ID_NO_PLACE_NAME = p.ID_NO_PLACE_NAME,
                    .PHONE = p.PHONE,
                    .TAXTATION_DATE = p.TAXTATION_DATE,
                    .TAXTATION_PLACE = p.TAXTATION_PLACE,
                    .BIRTH_CODE = p.BIRTH_CODE,
                    .QUYEN = p.QUYEN,
                    .BIRTH_NATION_ID = p.BIRTH_NATION_ID,
                    .BIRTH_PROVINCE_ID = p.BIRTH_PROVINCE_ID,
                    .BIRTH_DISTRICT_ID = p.BIRTH_DISTRICT_ID,
                    .BIRTH_WARD_ID = p.BIRTH_WARD_ID,
                    .BIRTH_NATION_NAME = n_ks.NAME_VN,
                    .BIRTH_PROVINCE_NAME = p_ks.NAME_VN,
                    .BIRTH_DISTRICT_NAME = d_ks.NAME_VN,
                    .BIRTH_WARD_NAME = w_ks.NAME_VN,
                    .GENDER = p.GENDER,
                    .GENDER_NAME = g.NAME_VN})
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If
            If _filter.RELATION_NAME <> "" Then
                query = query.Where(Function(p) p.RELATION_NAME.ToLower().Contains(_filter.RELATION_NAME.ToLower))
            End If
            If _filter.PROVINCE_NAME <> "" Then
                query = query.Where(Function(p) p.PROVINCE_NAME.ToLower().Contains(_filter.PROVINCE_NAME.ToLower))
            End If
            If _filter.FULLNAME <> "" Then
                query = query.Where(Function(p) p.FULLNAME.ToLower().Contains(_filter.FULLNAME.ToLower))
            End If
            If _filter.BIRTH_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.BIRTH_DATE = _filter.BIRTH_DATE)
            End If
            If _filter.ID_NO <> "" Then
                query = query.Where(Function(p) p.ID_NO.ToUpper().IndexOf(_filter.ID_NO.ToUpper) >= 0)
            End If
            If _filter.DEDUCT_REG IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_REG = _filter.DEDUCT_REG)
            End If
            If _filter.DEDUCT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_FROM >= _filter.DEDUCT_FROM)
            End If
            If _filter.DEDUCT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_TO <= _filter.DEDUCT_TO)
            End If
            If _filter.ADDRESS <> "" Then
                query = query.Where(Function(p) p.ADDRESS.ToLower().Contains(_filter.ADDRESS.ToLower))
            End If
            If _filter.ADDRESS_TT <> "" Then
                query = query.Where(Function(p) p.ADDRESS_TT.ToLower().Contains(_filter.ADDRESS_TT.ToLower))
            End If
            If _filter.REMARK <> "" Then
                query = query.Where(Function(p) p.REMARK.ToLower().Contains(_filter.REMARK.ToLower))
            End If
            If _filter.AD_DISTRICT_NAME <> "" Then
                query = query.Where(Function(p) p.AD_DISTRICT_NAME.ToLower().Contains(_filter.AD_DISTRICT_NAME.ToLower))
            End If
            If _filter.AD_PROVINCE_NAME <> "" Then
                query = query.Where(Function(p) p.AD_PROVINCE_NAME.ToLower().Contains(_filter.AD_PROVINCE_NAME.ToLower))
            End If
            If _filter.AD_VILLAGE <> "" Then
                query = query.Where(Function(p) p.AD_VILLAGE.ToLower().Contains(_filter.AD_VILLAGE.ToLower))
            End If
            If _filter.AD_WARD_NAME <> "" Then
                query = query.Where(Function(p) p.AD_WARD_NAME.ToLower().Contains(_filter.AD_WARD_NAME.ToLower))
            End If
            If _filter.TT_DISTRICT_NAME <> "" Then
                query = query.Where(Function(p) p.TT_DISTRICT_NAME.ToLower().Contains(_filter.TT_DISTRICT_NAME.ToLower))
            End If
            If _filter.TT_PROVINCE_NAME <> "" Then
                query = query.Where(Function(p) p.TT_PROVINCE_NAME.ToLower().Contains(_filter.TT_PROVINCE_NAME.ToLower))
            End If
            If _filter.TT_WARD_NAME <> "" Then
                query = query.Where(Function(p) p.TT_WARD_NAME.ToLower().Contains(_filter.TT_WARD_NAME.ToLower))
            End If
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteEmployeeFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_FAMILY)
        Try
            lst = (From p In Context.HU_FAMILY Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_FAMILY.DeleteObject(lst(i))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Hàm check trùng CMND của thân nhân.
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateFamily(ByVal _validate As FamilyDTO)
        Try
            If _validate.ID_NO <> "" Then
                Dim query = (From p In Context.HU_FAMILY
                         Where p.ID_NO.ToUpper = _validate.ID_NO.ToUpper And _
                         p.ID <> _validate.ID)

                Return query.Count = 0
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
            Throw ex
        End Try
    End Function

#End Region

#Region "FamilyEdit"

    Public Function InsertEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO,
                                             ByVal log As UserLog,
                                             ByRef gID As Decimal) As Boolean
        Try

            Dim objFamilyEditData As New HU_FAMILY_EDIT
            objFamilyEditData.ID = Utilities.GetNextSequence(Context, Context.HU_FAMILY_EDIT.EntitySet.Name)
            objFamilyEditData.EMPLOYEE_ID = objFamilyEdit.EMPLOYEE_ID
            objFamilyEditData.FULLNAME = objFamilyEdit.FULLNAME
            objFamilyEditData.RELATION_ID = objFamilyEdit.RELATION_ID
            objFamilyEditData.BIRTH_DATE = objFamilyEdit.BIRTH_DATE
            objFamilyEditData.TAXTATION = objFamilyEdit.TAXTATION
            objFamilyEditData.DEDUCT_REG = objFamilyEdit.DEDUCT_REG
            objFamilyEditData.ID_NO = objFamilyEdit.ID_NO
            objFamilyEditData.IS_DEDUCT = objFamilyEdit.IS_DEDUCT
            objFamilyEditData.DEDUCT_FROM = objFamilyEdit.DEDUCT_FROM
            objFamilyEditData.DEDUCT_TO = objFamilyEdit.DEDUCT_TO
            objFamilyEditData.REMARK = objFamilyEdit.REMARK
            objFamilyEditData.ADDRESS = objFamilyEdit.ADDRESS
            objFamilyEditData.STATUS = 0
            objFamilyEditData.REASON_UNAPROVE = objFamilyEdit.REASON_UNAPROVE
            objFamilyEditData.FK_PKEY = objFamilyEdit.FK_PKEY
            objFamilyEditData.CAREER = objFamilyEdit.CAREER
            objFamilyEditData.TITLE_NAME = objFamilyEdit.TITLE_NAME
            objFamilyEditData.PROVINCE_ID = objFamilyEdit.PROVINCE_ID
            objFamilyEditData.CERTIFICATE_CODE = objFamilyEdit.CERTIFICATE_CODE
            objFamilyEditData.CERTIFICATE_NUM = objFamilyEdit.CERTIFICATE_NUM
            objFamilyEditData.ADDRESS_TT = objFamilyEdit.ADDRESS_TT
            objFamilyEditData.AD_PROVINCE_ID = objFamilyEdit.AD_PROVINCE_ID
            objFamilyEditData.AD_DISTRICT_ID = objFamilyEdit.AD_DISTRICT_ID
            objFamilyEditData.AD_WARD_ID = objFamilyEdit.AD_WARD_ID
            objFamilyEditData.AD_VILLAGE = objFamilyEdit.AD_VILLAGE
            objFamilyEditData.TT_PROVINCE_ID = objFamilyEdit.TT_PROVINCE_ID
            objFamilyEditData.TT_DISTRICT_ID = objFamilyEdit.TT_DISTRICT_ID
            objFamilyEditData.TT_WARD_ID = objFamilyEdit.TT_WARD_ID
            objFamilyEditData.IS_OWNER = objFamilyEdit.IS_OWNER
            objFamilyEditData.IS_PASS = objFamilyEdit.IS_PASS

            objFamilyEditData.NATION_ID = objFamilyEdit.NATION_ID
            objFamilyEditData.ID_NO_DATE = objFamilyEdit.ID_NO_DATE
            objFamilyEditData.ID_NO_PLACE_NAME = objFamilyEdit.ID_NO_PLACE_NAME
            objFamilyEditData.PHONE = objFamilyEdit.PHONE
            objFamilyEditData.TAXTATION_DATE = objFamilyEdit.TAXTATION_DATE
            objFamilyEditData.TAXTATION_PLACE = objFamilyEdit.TAXTATION_PLACE
            objFamilyEditData.BIRTH_CODE = objFamilyEdit.BIRTH_CODE
            objFamilyEditData.QUYEN = objFamilyEdit.QUYEN
            objFamilyEditData.BIRTH_NATION_ID = objFamilyEdit.BIRTH_NATION_ID
            objFamilyEditData.BIRTH_PROVINCE_ID = objFamilyEdit.BIRTH_PROVINCE_ID
            objFamilyEditData.BIRTH_DISTRICT_ID = objFamilyEdit.BIRTH_DISTRICT_ID
            objFamilyEditData.BIRTH_WARD_ID = objFamilyEdit.BIRTH_WARD_ID
            objFamilyEditData.GENDER = objFamilyEdit.GENDER

            Context.HU_FAMILY_EDIT.AddObject(objFamilyEditData)
            Context.SaveChanges(log)
            gID = objFamilyEditData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objFamilyEditData As New HU_FAMILY_EDIT With {.ID = objFamilyEdit.ID}
        Try
            objFamilyEditData = (From p In Context.HU_FAMILY_EDIT Where p.ID = objFamilyEdit.ID).FirstOrDefault
            objFamilyEditData.EMPLOYEE_ID = objFamilyEdit.EMPLOYEE_ID
            objFamilyEditData.FULLNAME = objFamilyEdit.FULLNAME
            objFamilyEditData.RELATION_ID = objFamilyEdit.RELATION_ID
            objFamilyEditData.BIRTH_DATE = objFamilyEdit.BIRTH_DATE
            objFamilyEditData.DEDUCT_REG = objFamilyEdit.DEDUCT_REG
            objFamilyEditData.ID_NO = objFamilyEdit.ID_NO
            objFamilyEditData.TAXTATION = objFamilyEdit.TAXTATION
            objFamilyEditData.IS_DEDUCT = objFamilyEdit.IS_DEDUCT
            objFamilyEditData.DEDUCT_FROM = objFamilyEdit.DEDUCT_FROM
            objFamilyEditData.DEDUCT_TO = objFamilyEdit.DEDUCT_TO
            objFamilyEditData.REMARK = objFamilyEdit.REMARK
            objFamilyEditData.ADDRESS = objFamilyEdit.ADDRESS
            objFamilyEditData.REASON_UNAPROVE = objFamilyEdit.REASON_UNAPROVE
            objFamilyEditData.FK_PKEY = objFamilyEdit.FK_PKEY
            objFamilyEditData.CAREER = objFamilyEdit.CAREER
            objFamilyEditData.TITLE_NAME = objFamilyEdit.TITLE_NAME
            objFamilyEditData.PROVINCE_ID = objFamilyEdit.PROVINCE_ID
            objFamilyEditData.CERTIFICATE_CODE = objFamilyEdit.CERTIFICATE_CODE
            objFamilyEditData.CERTIFICATE_NUM = objFamilyEdit.CERTIFICATE_NUM
            objFamilyEditData.ADDRESS_TT = objFamilyEdit.ADDRESS_TT
            objFamilyEditData.AD_PROVINCE_ID = objFamilyEdit.AD_PROVINCE_ID
            objFamilyEditData.AD_DISTRICT_ID = objFamilyEdit.AD_DISTRICT_ID
            objFamilyEditData.AD_WARD_ID = objFamilyEdit.AD_WARD_ID
            objFamilyEditData.AD_VILLAGE = objFamilyEdit.AD_VILLAGE
            objFamilyEditData.TT_PROVINCE_ID = objFamilyEdit.TT_PROVINCE_ID
            objFamilyEditData.TT_DISTRICT_ID = objFamilyEdit.TT_DISTRICT_ID
            objFamilyEditData.TT_WARD_ID = objFamilyEdit.TT_WARD_ID
            objFamilyEditData.IS_OWNER = objFamilyEdit.IS_OWNER
            objFamilyEditData.IS_PASS = objFamilyEdit.IS_PASS

            objFamilyEditData.NATION_ID = objFamilyEdit.NATION_ID
            objFamilyEditData.ID_NO_DATE = objFamilyEdit.ID_NO_DATE
            objFamilyEditData.ID_NO_PLACE_NAME = objFamilyEdit.ID_NO_PLACE_NAME
            objFamilyEditData.PHONE = objFamilyEdit.PHONE
            objFamilyEditData.TAXTATION_DATE = objFamilyEdit.TAXTATION_DATE
            objFamilyEditData.TAXTATION_PLACE = objFamilyEdit.TAXTATION_PLACE
            objFamilyEditData.BIRTH_CODE = objFamilyEdit.BIRTH_CODE
            objFamilyEditData.QUYEN = objFamilyEdit.QUYEN
            objFamilyEditData.BIRTH_NATION_ID = objFamilyEdit.BIRTH_NATION_ID
            objFamilyEditData.BIRTH_PROVINCE_ID = objFamilyEdit.BIRTH_PROVINCE_ID
            objFamilyEditData.BIRTH_DISTRICT_ID = objFamilyEdit.BIRTH_DISTRICT_ID
            objFamilyEditData.BIRTH_WARD_ID = objFamilyEdit.BIRTH_WARD_ID
            objFamilyEditData.GENDER = objFamilyEdit.GENDER

            Context.SaveChanges(log)
            gID = objFamilyEditData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeFamilyEdit(ByVal _filter As FamilyEditDTO) As List(Of FamilyEditDTO)
        Dim query As ObjectQuery(Of FamilyEditDTO)
        Try
            query = (From p In Context.HU_FAMILY_EDIT
                     Group Join m In Context.HU_RELATIONSHIP_LIST On p.RELATION_ID Equals m.ID Into gGroup = Group
                     From p_g In gGroup.DefaultIfEmpty
                     Group Join n In Context.HU_PROVINCE On p.PROVINCE_ID Equals n.ID Into nGroup = Group
                     From n_g In nGroup.DefaultIfEmpty
                     From n_tt In Context.HU_NATION.Where(Function(F) F.ID = p.NATION_ID).DefaultIfEmpty
                     From n_ks In Context.HU_NATION.Where(Function(F) F.ID = p.BIRTH_NATION_ID).DefaultIfEmpty
                     From p_ks In Context.HU_PROVINCE.Where(Function(F) F.ID = p.BIRTH_PROVINCE_ID).DefaultIfEmpty
                     From d_ks In Context.HU_DISTRICT.Where(Function(F) F.ID = p.BIRTH_DISTRICT_ID).DefaultIfEmpty
                     From w_ks In Context.HU_WARD.Where(Function(F) F.ID = p.BIRTH_WARD_ID).DefaultIfEmpty
                     From g In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.GENDER).DefaultIfEmpty
                     Select New FamilyEditDTO With {
                         .ID = p.ID,
                         .ADDRESS = p.ADDRESS,
                         .EMPLOYEE_ID = p.EMPLOYEE_ID,
                         .FULLNAME = p.FULLNAME,
                         .RELATION_ID = p.RELATION_ID,
                         .RELATION_NAME = p_g.NAME,
                         .PROVINCE_ID = p.PROVINCE_ID,
                         .PROVINCE_NAME = n_g.NAME_VN,
                         .BIRTH_DATE = p.BIRTH_DATE,
                         .TAXTATION = p.TAXTATION,
                         .DEDUCT_REG = p.DEDUCT_REG,
                         .ID_NO = p.ID_NO,
                         .IS_DEDUCT = p.IS_DEDUCT,
                         .DEDUCT_FROM = p.DEDUCT_FROM,
                         .DEDUCT_TO = p.DEDUCT_TO,
                         .REMARK = p.REMARK,
                         .CERTIFICATE_NUM = p.CERTIFICATE_NUM,
                         .CERTIFICATE_CODE = p.CERTIFICATE_CODE,
                         .ADDRESS_TT = p.ADDRESS_TT,
                         .AD_PROVINCE_ID = p.AD_PROVINCE_ID,
                         .AD_DISTRICT_ID = p.AD_DISTRICT_ID,
                         .AD_WARD_ID = p.AD_WARD_ID,
                         .AD_VILLAGE = p.AD_VILLAGE,
                         .TT_PROVINCE_ID = p.TT_PROVINCE_ID,
                         .TT_DISTRICT_ID = p.TT_DISTRICT_ID,
                         .TT_WARD_ID = p.TT_WARD_ID,
                         .IS_OWNER = p.IS_OWNER,
                         .IS_PASS = p.IS_PASS,
                         .REASON_UNAPROVE = p.REASON_UNAPROVE,
                         .FK_PKEY = p.FK_PKEY,
                         .TITLE_NAME = p.TITLE_NAME,
                         .CAREER = p.CAREER,
                        .NATION_ID = p.NATION_ID,
                        .NATION_NAME = n_tt.NAME_VN,
                        .ID_NO_DATE = p.ID_NO_DATE,
                        .ID_NO_PLACE_NAME = p.ID_NO_PLACE_NAME,
                        .PHONE = p.PHONE,
                        .TAXTATION_DATE = p.TAXTATION_DATE,
                        .TAXTATION_PLACE = p.TAXTATION_PLACE,
                        .BIRTH_CODE = p.BIRTH_CODE,
                        .QUYEN = p.QUYEN,
                        .BIRTH_NATION_ID = p.BIRTH_NATION_ID,
                        .BIRTH_PROVINCE_ID = p.BIRTH_PROVINCE_ID,
                        .BIRTH_DISTRICT_ID = p.BIRTH_DISTRICT_ID,
                        .BIRTH_WARD_ID = p.BIRTH_WARD_ID,
                        .BIRTH_NATION_NAME = n_ks.NAME_VN,
                        .BIRTH_PROVINCE_NAME = p_ks.NAME_VN,
                        .BIRTH_DISTRICT_NAME = d_ks.NAME_VN,
                        .BIRTH_WARD_NAME = w_ks.NAME_VN,
                         .GENDER = p.GENDER,
                         .GENDER_NAME = g.NAME_VN,
                         .STATUS = p.STATUS,
                         .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                           If(p.STATUS = 1, "Chờ phê duyệt",
                                              If(p.STATUS = 2, "Phê duyệt",
                                                 If(p.STATUS = 3, "Không phê duyệt", ""))))})

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.STATUS <> "" Then
                query = query.Where(Function(p) p.STATUS = _filter.STATUS)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetApproveEmployeeCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                    From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID).DefaultIfEmpty
                    From OT1 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE).DefaultIfEmpty
                    From OT2 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.LEVEL_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                       Where p.STATUS = 1
                       Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                               .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                               .EMPLOYEE_NAME = e.FULLNAME_VN,
                              .ID = p.ID,
                        .EMPLOYEE_ID = p.EMPLOYEE_ID,
                        .FROM_DATE = p.FROM_DATE,
                        .TO_DATE = p.TO_DATE,
                         .RECEIVE_DEGREE_DATE = p.RECEIVE_DEGREE_DATE,
                        .YEAR_GRA = p.YEAR_GRA,
                        .NAME_SHOOLS = p.NAME_SHOOLS,
                        .FORM_TRAIN_ID = p.FORM_TRAIN_ID,
                        .FORM_TRAIN_NAME = ot.NAME_VN,
                        .SPECIALIZED_TRAIN = p.SPECIALIZED_TRAIN,
                        .RESULT_TRAIN = p.RESULT_TRAIN,
                        .CERTIFICATE = OT1.NAME_VN,
                        .CERTIFICATE_ID = p.CERTIFICATE,
                        .EFFECTIVE_DATE_FROM = p.EFFECTIVE_DATE_FROM,
                        .EFFECTIVE_DATE_TO = p.EFFECTIVE_DATE_TO,
                        .CREATED_BY = p.CREATED_BY,
                         .IS_RENEWED = p.IS_RENEW,
                               .CONTENT_TRAIN = p.CONTENT_TRAIN,
                               .LEVEL_ID = p.LEVEL_ID,
                               .LEVEL_NAME = OT2.NAME_VN,
                               .SCORE = p.SCORE,
                               .CODE_CERTIFICATE = p.CODE_CERTIFICATE,
                               .REMARK = p.REMARK,
                         .TYPE_TRAIN_ID = p.TYPE_TRAIN_ID,
                         .TYPE_TRAIN_NAME = p.TYPE_TRAIN_NAME,
                        .CREATED_DATE = p.CREATED_DATE,
                        .CREATED_LOG = p.CREATED_LOG,
                        .MODIFIED_BY = p.MODIFIED_BY,
                        .MODIFIED_DATE = p.MODIFIED_DATE,
                        .MODIFIED_LOG = p.MODIFIED_LOG,
                        .REASON_UNAPROVE = p.REASON_UNAPROVE,
                        .FK_PKEY = p.FK_PKEY,
                         .UPLOAD_FILE = p.UPLOAD_FILE,
                         .FILE_NAME = p.FILE_NAME,
                        .STATUS = p.STATUS,
                        .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                           If(p.STATUS = 1, "Chờ phê duyệt",
                                              If(p.STATUS = 2, "Phê duyệt",
                                                 If(p.STATUS = 3, "Không phê duyệt", ""))))})
            Return query.ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetApproveFamilyEdit(ByVal _filter As FamilyEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of FamilyEditDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = (From p In Context.HU_FAMILY_EDIT
                         From p_g In Context.HU_RELATIONSHIP_LIST.Where(Function(f) p.RELATION_ID = f.ID).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Where p.STATUS = 1
                        Select New FamilyEditDTO With {
                            .ID = p.ID,
                            .ADDRESS = p.ADDRESS,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .FULLNAME = p.FULLNAME,
                            .RELATION_ID = p.RELATION_ID,
                            .RELATION_NAME = p_g.NAME,
                            .BIRTH_DATE = p.BIRTH_DATE,
                            .DEDUCT_REG = p.DEDUCT_REG,
                            .ID_NO = p.ID_NO,
                            .IS_DEDUCT = p.IS_DEDUCT,
                            .DEDUCT_FROM = p.DEDUCT_FROM,
                            .DEDUCT_TO = p.DEDUCT_TO,
                            .REMARK = p.REMARK,
                            .TITLE_ID = e.TITLE_ID,
                            .TITLE_NAME = p.TITLE_NAME,
                            .CAREER = p.CAREER,
                            .FK_PKEY = p.FK_PKEY,
                            .STATUS = p.STATUS,
                            .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                              If(p.STATUS = 1, "Chờ phê duyệt",
                                                 If(p.STATUS = 2, "Phê duyệt",
                                                    If(p.STATUS = 3, "Không phê duyệt", ""))))})


            If _filter.DEDUCT_REG IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_REG = _filter.DEDUCT_REG)
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.FULLNAME IsNot Nothing Then
                query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
            End If

            If _filter.ADDRESS IsNot Nothing Then
                query = query.Where(Function(p) p.ADDRESS.ToUpper.Contains(_filter.ADDRESS.ToUpper))
            End If

            If _filter.RELATION_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.RELATION_NAME.ToUpper.Contains(_filter.RELATION_NAME.ToUpper))
            End If

            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            If _filter.ID_NO IsNot Nothing Then
                query = query.Where(Function(p) p.ID_NO.ToUpper.Contains(_filter.ID_NO.ToUpper))
            End If

            If _filter.REMARK IsNot Nothing Then
                query = query.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If _filter.DEDUCT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_TO = _filter.DEDUCT_TO)
            End If

            If _filter.DEDUCT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_FROM = _filter.DEDUCT_FROM)
            End If

            If _filter.IS_DEDUCT IsNot Nothing Then
                query = query.Where(Function(p) p.IS_DEDUCT = _filter.IS_DEDUCT)
            End If
            Dim working = query

            working = working.OrderBy(Sorts)
            Total = working.Count
            working = working.Skip(PageIndex * PageSize).Take(PageSize)

            Return working.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteEmployeeFamilyEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_FAMILY_EDIT)
        Try
            lst = (From p In Context.HU_FAMILY_EDIT Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_FAMILY_EDIT.DeleteObject(lst(i))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckExistFamilyEdit(ByVal pk_key As Decimal) As FamilyEditDTO
        Try
            Dim query = (From p In Context.HU_FAMILY_EDIT
                         Where p.STATUS <> 2 And p.FK_PKEY = pk_key
                         Select New FamilyEditDTO With {
                             .ID = p.ID,
                             .STATUS = p.STATUS}).FirstOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function SendEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        Try
            Dim lstObj = (From p In Context.HU_FAMILY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.SEND_DATE = Date.Now
                item.STATUS = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                                   status As String,
                                                   ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_FAMILY_EDIT)
        Dim sStatus() As String = status.Split(":")

        Try
            lst = (From p In Context.HU_FAMILY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)

                If sStatus(0) = 2 Then
                    If item.FK_PKEY IsNot Nothing Then
                        Dim objFamilyData As HU_FAMILY
                        objFamilyData = (From p In Context.HU_FAMILY Where p.ID = item.FK_PKEY).FirstOrDefault
                        objFamilyData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objFamilyData.FULLNAME = item.FULLNAME
                        objFamilyData.RELATION_ID = item.RELATION_ID
                        objFamilyData.BIRTH_DATE = item.BIRTH_DATE
                        objFamilyData.TAXTATION = item.TAXTATION
                        objFamilyData.DEDUCT_REG = item.DEDUCT_REG
                        objFamilyData.ID_NO = item.ID_NO
                        objFamilyData.IS_DEDUCT = item.IS_DEDUCT
                        objFamilyData.DEDUCT_FROM = item.DEDUCT_FROM
                        objFamilyData.DEDUCT_TO = item.DEDUCT_TO
                        objFamilyData.REMARK = item.REMARK
                        objFamilyData.ADDRESS = item.ADDRESS

                        ' 20190520 CanhNX: Edit cho lưu Nguyên quán, Nghề nghiệp, Chức danh
                        objFamilyData.CAREER = item.CAREER
                        objFamilyData.TITLE_NAME = item.TITLE_NAME
                        objFamilyData.PROVINCE_ID = item.PROVINCE_ID

                    Else
                        Dim objFamilyData As New HU_FAMILY
                        objFamilyData.ID = Utilities.GetNextSequence(Context, Context.HU_FAMILY.EntitySet.Name)
                        objFamilyData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objFamilyData.FULLNAME = item.FULLNAME
                        objFamilyData.RELATION_ID = item.RELATION_ID
                        objFamilyData.BIRTH_DATE = item.BIRTH_DATE
                        objFamilyData.DEDUCT_REG = item.DEDUCT_REG
                        objFamilyData.ID_NO = item.ID_NO
                        objFamilyData.IS_DEDUCT = item.IS_DEDUCT
                        objFamilyData.DEDUCT_FROM = item.DEDUCT_FROM
                        objFamilyData.TAXTATION = item.TAXTATION
                        objFamilyData.DEDUCT_TO = item.DEDUCT_TO
                        objFamilyData.REMARK = item.REMARK
                        objFamilyData.ADDRESS = item.ADDRESS

                        ' 20190520 CanhNX: Edit cho lưu Nguyên quán, Nghề nghiệp, Chức danh
                        objFamilyData.CAREER = item.CAREER
                        objFamilyData.TITLE_NAME = item.TITLE_NAME
                        objFamilyData.PROVINCE_ID = item.PROVINCE_ID

                        Context.HU_FAMILY.AddObject(objFamilyData)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusEmployeeCetificateEdit(ByVal lstID As List(Of Decimal),
                                                   status As String,
                                                   ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_PRO_TRAIN_OUT_COMPANY_EDIT)
        Dim sStatus() As String = status.Split(":")
        Try
            lst = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)
                If sStatus(0) = 2 Then
                    If item.FK_PKEY IsNot Nothing Then
                        Dim objFamilyData As HU_PRO_TRAIN_OUT_COMPANY
                        objFamilyData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.ID = item.FK_PKEY).FirstOrDefault
                        objFamilyData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objFamilyData.YEAR_GRA = item.YEAR_GRA
                        objFamilyData.NAME_SHOOLS = item.NAME_SHOOLS
                        objFamilyData.FORM_TRAIN_ID = item.FORM_TRAIN_ID
                        objFamilyData.SPECIALIZED_TRAIN = item.SPECIALIZED_TRAIN
                        objFamilyData.RESULT_TRAIN = item.RESULT_TRAIN
                        objFamilyData.CERTIFICATE = item.CERTIFICATE
                        objFamilyData.EFFECTIVE_DATE_FROM = item.EFFECTIVE_DATE_FROM
                        objFamilyData.EFFECTIVE_DATE_TO = item.EFFECTIVE_DATE_TO
                        objFamilyData.FROM_DATE = item.FROM_DATE
                        objFamilyData.TO_DATE = item.TO_DATE
                        objFamilyData.UPLOAD_FILE = item.UPLOAD_FILE
                        objFamilyData.FILE_NAME = item.FILE_NAME
                        'objFamilyData.TYPE_TRAIN_ID = item.TYPE_TRAIN_ID
                        objFamilyData.TYPE_TRAIN_NAME = item.TYPE_TRAIN_NAME
                        objFamilyData.CONTENT_LEVEL = item.CONTENT_TRAIN
                        objFamilyData.CERTIFICATE_CODE = item.CODE_CERTIFICATE
                        objFamilyData.LEVEL_ID = item.LEVEL_ID
                        objFamilyData.NOTE = item.REMARK
                        objFamilyData.POINT_LEVEL = item.SCORE
                        objFamilyData.RECEIVE_DEGREE_DATE = item.RECEIVE_DEGREE_DATE
                        objFamilyData.IS_RENEWED = item.IS_RENEW
                    Else
                        Dim objFamilyData As New HU_PRO_TRAIN_OUT_COMPANY
                        objFamilyData.ID = Utilities.GetNextSequence(Context, Context.HU_PRO_TRAIN_OUT_COMPANY.EntitySet.Name)
                        objFamilyData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objFamilyData.YEAR_GRA = item.YEAR_GRA
                        objFamilyData.NAME_SHOOLS = item.NAME_SHOOLS
                        objFamilyData.FORM_TRAIN_ID = item.FORM_TRAIN_ID
                        objFamilyData.SPECIALIZED_TRAIN = item.SPECIALIZED_TRAIN
                        objFamilyData.RESULT_TRAIN = item.RESULT_TRAIN
                        objFamilyData.CERTIFICATE = item.CERTIFICATE
                        objFamilyData.EFFECTIVE_DATE_FROM = item.EFFECTIVE_DATE_FROM
                        objFamilyData.EFFECTIVE_DATE_TO = item.EFFECTIVE_DATE_TO
                        objFamilyData.FROM_DATE = item.FROM_DATE
                        objFamilyData.TO_DATE = item.TO_DATE
                        objFamilyData.UPLOAD_FILE = item.UPLOAD_FILE
                        objFamilyData.FILE_NAME = item.FILE_NAME
                        'objFamilyData.TYPE_TRAIN_ID = item.TYPE_TRAIN_ID
                        objFamilyData.TYPE_TRAIN_NAME = item.TYPE_TRAIN_NAME
                        objFamilyData.CONTENT_LEVEL = item.CONTENT_TRAIN
                        objFamilyData.CERTIFICATE_CODE = item.CODE_CERTIFICATE
                        objFamilyData.LEVEL_ID = item.LEVEL_ID
                        objFamilyData.NOTE = item.REMARK
                        objFamilyData.POINT_LEVEL = item.SCORE
                        objFamilyData.RECEIVE_DEGREE_DATE = item.RECEIVE_DEGREE_DATE
                        objFamilyData.IS_RENEWED = item.IS_RENEW
                        Context.HU_PRO_TRAIN_OUT_COMPANY.AddObject(objFamilyData)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

End Class
