Imports Aspose.Cells
Imports Framework.UI
Imports System.IO
Imports Attendance.AttendanceBusiness
Public Class Export
    Inherits System.Web.UI.Page

#Region "Page"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Select Case Request.Params("id")
                    Case "TR_ASSESSMENT_RESULT"
                        ' TR_ASSESSMENT_RESULT()
                    Case "Template_ExportProgramDeclare"
                        Template_ExportProgramDeclare()
                    Case "Form_Suggest_Intern"
                        Form_Suggest_Intern()
                    Case "TR_ASSESSMENT_RESULT_ERROR"
                        TR_ASSESSMENT_RESULT_ERROR()
                    Case "TR_REQUEST_EMPLOYEE"
                        TR_REQUEST_EMPLOYEE()
                    Case "TR_REQUEST_EMPLOYEE_ERROR"
                        TR_REQUEST_EMPLOYEE_ERROR()
                    Case "RC_CANDIDATE_IMPORT"
                        RC_CANDIDATE_IMPORT()
                    Case "RC_CANDIDATE_IMPORT_ERROR"
                        RC_CANDIDATE_IMPORT_ERROR()
                    Case "ManIOImport"
                        ManIOImport()
                    Case "Time_TimeSheetCCT"
                        Time_TimeSheetCCT()
                    Case "Template_importTimesheet_CTT_Error"
                        Time_TimeSheetCCT_Error()
                    Case "Template_importTimesheet_CTT_Error1"
                        Time_TimeSheetCCT_Error1()
                    Case "WorkShiftImport"
                        WorkShiftImport()
                    Case "Template_ImportDMVS"
                        Template_ImportDMVS()
                    Case "Template_ImportOT"
                        Template_ImportOT()
                    Case "Template_DeclareOT"
                        Template_DeclareOT()
                    Case "Template_ImportShift_error"
                        Template_ImportShift_error()
                    Case "Template_importIO_error"
                        Template_importIO_error()
                    Case "Template_ImportDMVS_error"
                        Template_ImportDMVS_error()
                    Case "Template_ImportOT_error"
                        Template_ImportOT_error()
                    Case "Template_GiaiTrinhNgayCong_error"
                        Template_GiaiTrinhNgayCong_error()
                    Case "Template_DeclareOT_error"
                        Template_DeclareOT_error()
                    Case "Template_ImportSalary"
                        Template_ImportSalary()
                        'Case "Template_ImportSeniorityProcess"
                        '    Template_ImportSeniorityProcess()
                        'Case "Template_ImportSeniorityProcess_error"
                        Template_ImportSeniorityProcess_error()
                    Case "Template_ImportDM"
                        Template_ImportDM()
                    Case "Template_DMNoiKhamChuaBenh_ERROR"
                        Template_ImportDMNK_ERROR()
                    Case "Import_InfoIns"
                        Import_InfoIns()
                    Case "Template_ImportInfoIns_error"
                        Template_ImportInfoIns_error()
                    Case "TokhaiA01"
                        TokhaiA01()
                    Case "Template_SingDefault"
                        Template_SingDefault()
                    Case "Import_SingDefault_Error"
                        Import_SingDefault_Error()
                    Case "Template_ImportDeclareTimeRice"
                        Template_DeclareTimeRice()
                    Case "Import_DeclareTimeRice_Error"
                        Import_DeclareTimeRice_Error()
                    Case "Template_ImportTimeSheetRice"
                        Template_ImportTimeSheetRice()
                    Case "Import_TimeSheetRice_Error"
                        Import_TimeSheetRice_Error()
                    Case "Template_ImportSwipeData"
                        Template_ImportSwipeData()
                    Case "Import_SwipeData_Error"
                        Import_SwipeData_Error()
                    Case "HU_ChangeInfo"
                        HU_ChangeInfo()
                    Case "HU_ChangeInfo_Error"
                        HU_ChangeInfo_Error()
                    Case "Template_Register"
                        Template_Register()
                    Case "Template_Register_Error"
                        Template_Register_Error()
                    Case "Time_TimeSheet_Rice"
                        Time_TimeSheet_Rice()
                    Case "Time_TimeSheet_OT"
                        Time_TimeSheet_OT()
                    Case "IMPORT_SALARYPLANNING"
                        IMPORT_SALARYPLANNING()
                    Case "IMPORT_SALARYPLANNING_ERROR"
                        IMPORT_SALARYPLANNING_ERROR()
                    Case "Template_DeclareEntitlement"
                        Template_DeclareEntitlement()
                    Case "Template_DeclareEntitlement_error"
                        Template_DeclareEntitlement_error()
                    Case "Import_INS_SUNCARE"
                        Import_INS_SUNCARE()
                    Case "Import_INS_SUNCARE_ERROR"
                        Import_INS_SUNCARE_error()
                    Case "Template_ImportSalary"
                        Template_ImportSalary()
                    Case "Template_ImportThuongHQCV"
                        Template_ImportThuongHQCV()
                    Case "Template_ImportQuyThuongHQCV"
                        Template_ImportQuyThuongHQCV()
                    Case "Template_ImportThueTNCN"
                        Template_ImportQTThueTNCN()
                    Case "Template_ImportBonus"
                        Template_ImportBonus()
                    Case "Template_ImportSalary_FundMapping"
                        Template_ImportSalary_FundMapping()
                    Case "HU_ANNUALLEAVE_PLANS_ERROR"
                        HU_ANNUALLEAVE_PLANS_ERROR()
                    Case "Template_ImportHoSoLuong"
                        Template_ImportHoSoLuong()
                    Case "Timesheet_machineExport"
                        Timesheet_machineExport()
                    Case "Template_yeucautuyendung_Error"
                        Template_yeucautuyendung_Error()
                    Case "Export_Determine"
                        Export_Determine()
                    Case "Template_Allowance"
                        Template_Allowance()
                End Select
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
            End Try
        End If
    End Sub

#End Region

#Region "Process"
    Private Sub Template_ImportHoSoLuong()
        Dim rep As New Profile.ProfileBusinessRepository
        'Dim param As New Profile.ProfileBusiness.ParamDTO
        Try
            'Dim is_disolve = Request.Params("IS_DISSOLVE")
            'Dim org_id = Decimal.Parse(Request.Params("ORG_ID"))
            'param.ORG_ID = org_id
            'param.IS_DISSOLVE = is_disolve
            Dim dsData As DataSet = rep.GetHoSoLuongImport()
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            dsData.Tables(2).TableName = "Table2"
            dsData.Tables(3).TableName = "Table3"
            dsData.Tables(4).TableName = "Table4"
            dsData.Tables(5).TableName = "Table5"
            rep.Dispose()
            ExportTemplate("Payroll/Business/TEMP_IMPORT_HOSOLUONG.xlsx",
                                      dsData, Nothing, "Template_HoSoLuong_" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub TR_ASSESSMENT_RESULT()
    '    Try
    '        Dim rep As New Training.TrainingRepository
    '        Dim lst = rep.GetAssessmentResultByID(New Training.TrainingBusiness.AssessmentResultDtlDTO With {
    '                                              .EMPLOYEE_ID = Decimal.Parse(Request.Params("EMPLOYEE_ID")),
    '                                              .TR_CHOOSE_FORM_ID = Request.Params("TR_CHOOSE_FORM_ID")})
    '        Dim dtData As DataTable = lst.ToTable
    '        dtData.TableName = "DATA"
    '        Dim dtVar As DataTable = dtData.Clone
    '        If dtData.Rows.Count > 0 Then
    '            dtVar.ImportRow(dtData.Rows(0))
    '        End If
    '        ExportTemplate("Training\Import\AssessmentResult.xls", _
    '                                  dtData, dtVar, _
    '                                  "TemplateImport_" & Format(Date.Now, "yyyyMMdd"))
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub Template_yeucautuyendung_Error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Recruitment\Import\Template_yeucautuyendung_Error.xls", _
                                      dtData, dtVar, _
                                      "TemplateImportError_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Export_Determine()
        Try
            Dim dsData As DataSet = Session("EXPORT_DETERMINE")
            ExportTemplate("Recruitment\Import\Export_Determine.xls", dsData, Nothing, "Determine" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TR_ASSESSMENT_RESULT_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Training\Import\AssessmentResult_Error.xls", _
                                      dtData, dtVar, _
                                      "TemplateImportError_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TR_REQUEST_EMPLOYEE()
        Try
            Dim dtData As New DataTable
            dtData.TableName = "DATA"
            ExportTemplate("Training\Import\RequestEmployee.xls", _
                                      dtData, Nothing, _
                                      "TemplateImport_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TR_REQUEST_EMPLOYEE_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Training\Import\RequestEmployee_Error.xls", _
                                      dtData, Nothing, _
                                      "TemplateImportError_" & Format(Date.Now, "yyyyMMdd"))

            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub RC_CANDIDATE_IMPORT()
        Try
            GetInformationLists()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetInformationLists()
        Dim repStore As New Recruitment.RecruitmentStoreProcedure
        Dim dsDB As New DataSet
        Dim dtTonGiao, dtMoiQH, dtQuocGia, dtTinh, dtHuyen, dtXa, dtChuyenNganh,
            dtChuyenMon, dtHonNhan, dtDanToc, dtLogo, dtGender, dtSchool, dtTDVH As New DataTable
        Try
            dsDB = repStore.GET_ALL_LIST(1)
            If dsDB.Tables.Count > 0 Then

                'Ton Giao
                If dsDB.Tables(0) IsNot Nothing AndAlso dsDB.Tables(0).Rows.Count > 0 Then
                    dtTonGiao = dsDB.Tables(0)
                End If

                'Moi Quan He
                If dsDB.Tables(1) IsNot Nothing AndAlso dsDB.Tables(1).Rows.Count > 0 Then
                    dtMoiQH = dsDB.Tables(1)
                End If

                'Quoc Gia
                If dsDB.Tables(2) IsNot Nothing AndAlso dsDB.Tables(2).Rows.Count > 0 Then
                    dtQuocGia = dsDB.Tables(2)
                End If

                'Tinh(Thanh pho)
                If dsDB.Tables(3) IsNot Nothing AndAlso dsDB.Tables(3).Rows.Count > 0 Then
                    dtTinh = dsDB.Tables(3)
                End If

                'Huyen(Quan)
                If dsDB.Tables(4) IsNot Nothing AndAlso dsDB.Tables(4).Rows.Count > 0 Then
                    dtHuyen = dsDB.Tables(4)
                End If

                'Xa(Phuong)
                If dsDB.Tables(5) IsNot Nothing AndAlso dsDB.Tables(5).Rows.Count > 0 Then
                    dtXa = dsDB.Tables(5)
                End If

                'Chuyen Nganh
                If dsDB.Tables(6) IsNot Nothing AndAlso dsDB.Tables(6).Rows.Count > 0 Then
                    dtChuyenNganh = dsDB.Tables(6)
                End If

                'Chuyen Mon
                If dsDB.Tables(7) IsNot Nothing AndAlso dsDB.Tables(7).Rows.Count > 0 Then
                    dtChuyenMon = dsDB.Tables(7)
                End If

                'Tinh Trang Hon Nhan
                If dsDB.Tables(8) IsNot Nothing AndAlso dsDB.Tables(8).Rows.Count > 0 Then
                    dtHonNhan = dsDB.Tables(8)
                End If

                'Dan Toc
                If dsDB.Tables(9) IsNot Nothing AndAlso dsDB.Tables(9).Rows.Count > 0 Then
                    dtDanToc = dsDB.Tables(9)
                End If
                'file logo
                If dsDB.Tables(10) IsNot Nothing AndAlso dsDB.Tables(10).Rows.Count > 0 Then
                    dtLogo = dsDB.Tables(10)
                End If
                'gioi tinh
                If dsDB.Tables(11) IsNot Nothing AndAlso dsDB.Tables(11).Rows.Count > 0 Then
                    dtGender = dsDB.Tables(11)
                End If
                'truong hoc 
                If dsDB.Tables(12) IsNot Nothing AndAlso dsDB.Tables(12).Rows.Count > 0 Then
                    dtSchool = dsDB.Tables(12)
                End If
                'trinh do van hoa
                If dsDB.Tables(13) IsNot Nothing AndAlso dsDB.Tables(13).Rows.Count > 0 Then
                    dtTDVH = dsDB.Tables(13)
                End If

                ExportTemplate("Recruitment\Import\Import_ungvien_acv.xls", dtTonGiao, dtMoiQH, dtQuocGia, dtTinh, dtHuyen, dtXa, dtChuyenNganh, dtChuyenMon, dtHonNhan, dtDanToc, dtLogo, dtGender, dtSchool, dtTDVH, "Import_UngVien")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dt1 As DataTable,
                                                    ByVal dt2 As DataTable,
                                                    ByVal dt3 As DataTable,
                                                    ByVal dt4 As DataTable,
                                                    ByVal dt5 As DataTable,
                                                    ByVal dt6 As DataTable,
                                                    ByVal dt7 As DataTable,
                                                    ByVal dt8 As DataTable,
                                                    ByVal dt9 As DataTable,
                                                    ByVal dt10 As DataTable,
                                                    ByVal dt11 As DataTable,
                                                    ByVal dt12 As DataTable,
                                                    ByVal dt13 As DataTable,
                                                    ByVal dt14 As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)

            If dt1 IsNot Nothing Then
                dt1.TableName = "TableTonGiao"
                designer.SetDataSource(dt1)
            End If

            If dt2 IsNot Nothing Then
                dt2.TableName = "TableMoiQH"
                designer.SetDataSource(dt2)
            End If

            If dt3 IsNot Nothing Then
                dt3.TableName = "TableQuocGia"
                designer.SetDataSource(dt3)
            End If

            If dt4 IsNot Nothing Then
                dt4.TableName = "TableTinh"
                designer.SetDataSource(dt4)
            End If

            If dt5 IsNot Nothing Then
                dt5.TableName = "TableHuyen"
                designer.SetDataSource(dt5)
            End If

            If dt6 IsNot Nothing Then
                dt6.TableName = "TableXa"
                designer.SetDataSource(dt6)
            End If

            If dt7 IsNot Nothing Then
                dt7.TableName = "TableChuyenNganh"
                designer.SetDataSource(dt7)
            End If

            If dt8 IsNot Nothing Then
                dt8.TableName = "TableChuyenMon"
                designer.SetDataSource(dt8)
            End If

            If dt9 IsNot Nothing Then
                dt9.TableName = "TableHonNhan"
                designer.SetDataSource(dt9)
            End If

            If dt10 IsNot Nothing Then
                dt10.TableName = "TableDanToc"
                designer.SetDataSource(dt10)
            End If
            If dt12 IsNot Nothing Then
                dt12.TableName = "TableGioiTinh"
                designer.SetDataSource(dt12)
            End If
            If dt13 IsNot Nothing Then
                dt13.TableName = "TableSchool"
                designer.SetDataSource(dt13)
            End If
            If dt14 IsNot Nothing Then
                dt14.TableName = "TableTDVH"
                designer.SetDataSource(dt14)
            End If
            'vnm hien tai k dung logo nen rem doan code nay lai
            'If dt11 IsNot Nothing Then
            '    designer.SetDataSource(dt11)
            '    Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
            '    Dim str As String = sourcePath + dt11.Rows(0)("ATTACH_FILE_LOGO").ToString + dt11.Rows(0)("FILE_LOGO").ToString
            '    Dim worksheet As Aspose.Cells.Worksheet = designer.Workbook.Worksheets(0)
            '    Dim b As Byte() = File.ReadAllBytes(str)
            '    Dim ms As New System.IO.MemoryStream(b)
            '    Dim pictureIndex As Integer = worksheet.Pictures.Add(1, 1, ms)

            '    'Accessing the newly added picture
            '    Dim picture As Aspose.Cells.Drawing.Picture = worksheet.Pictures(pictureIndex)

            '    'Positioning the picture proportional to row height and colum width
            '    picture.Width = 400
            '    picture.Height = 120

            'End If

            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    'Private Sub RC_CANDIDATE_IMPORT()
    '    Try
    '        Dim rep As New Recruitment.RecruitmentRepository
    '        Dim ds = rep.GetCandidateImport()
    '        ds.Tables(0).TableName = "DATA"
    '        Dim i As Integer = 1
    '        For Each dt As DataTable In ds.Tables
    '            If dt.TableName <> "DATA" Then
    '                dt.TableName = "DATA" & i.ToString
    '                i += 1
    '            Else
    '                ' Tạo dữ liệu vlookup
    '                For i1 = 1 To 500
    '                    Dim row = dt.NewRow
    '                    dt.Rows.Add(row)
    '                Next
    '            End If
    '        Next
    '        ExportTemplate("Recruitment\Import\Candidate.xls", _
    '                                  ds, Nothing, _
    '                                  "TemplateImport_" & Format(Date.Now, "yyyyMMdd"))
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub RC_CANDIDATE_IMPORT_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")

            ExportTemplate("Recruitment\Import\Candidate_Error.xls", _
                                      dtData, Nothing, _
                                      "TemplateImportError_" & Format(Date.Now, "yyyyMMdd"))

            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ManIOImport()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))

            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = False
            obj.P_EXPORT_TYPE = 1
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            ExportTemplate("Attendance\Import\Template_importIO.xls", _
                                      dsData, Nothing, _
                                      "Template_importIO" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Time_TimeSheetCCT()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.P_EXPORT_TYPE = 10
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            If Not String.IsNullOrEmpty(Request.Params("orgid")) Then
                obj.ORG_ID = Decimal.Parse(Request.Params("orgid"))
            Else
                obj.ORG_ID = 0
            End If
            obj.IS_DISSOLVE = 0
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table1"
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(System.String))
            dtvariable.Columns.Add("D2", GetType(System.String))
            dtvariable.Columns.Add("D3", GetType(System.String))
            dtvariable.Columns.Add("D4", GetType(System.String))
            dtvariable.Columns.Add("D5", GetType(System.String))
            dtvariable.Columns.Add("D6", GetType(System.String))
            dtvariable.Columns.Add("D7", GetType(System.String))
            dtvariable.Columns.Add("D8", GetType(System.String))
            dtvariable.Columns.Add("D9", GetType(System.String))
            dtvariable.Columns.Add("D10", GetType(System.String))

            dtvariable.Columns.Add("D11", GetType(System.String))
            dtvariable.Columns.Add("D12", GetType(System.String))
            dtvariable.Columns.Add("D13", GetType(System.String))
            dtvariable.Columns.Add("D14", GetType(System.String))
            dtvariable.Columns.Add("D15", GetType(System.String))
            dtvariable.Columns.Add("D16", GetType(System.String))
            dtvariable.Columns.Add("D17", GetType(System.String))
            dtvariable.Columns.Add("D18", GetType(System.String))
            dtvariable.Columns.Add("D19", GetType(System.String))
            dtvariable.Columns.Add("D20", GetType(System.String))

            dtvariable.Columns.Add("D21", GetType(System.String))
            dtvariable.Columns.Add("D22", GetType(System.String))
            dtvariable.Columns.Add("D23", GetType(System.String))
            dtvariable.Columns.Add("D24", GetType(System.String))
            dtvariable.Columns.Add("D25", GetType(System.String))
            dtvariable.Columns.Add("D26", GetType(System.String))
            dtvariable.Columns.Add("D27", GetType(System.String))
            dtvariable.Columns.Add("D28", GetType(System.String))
            dtvariable.Columns.Add("D29", GetType(System.String))
            dtvariable.Columns.Add("D30", GetType(System.String))
            dtvariable.Columns.Add("D31", GetType(System.String))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = lsData.START_DATE
            Dim row = dtvariable.NewRow
            Dim I As Integer = 1
            While dDay <= lsData.END_DATE
                row("D" & I) = dDay.Value.ToString("dd") & "/" & dDay.Value.ToString("MM")
                dDay = dDay.Value.AddDays(1)
                I += 1
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "Variable"
            Dim dtData = CType(Session("EXPORTTIMESHEETDAILY"), DataTable)
            dtData = dtData.Copy
            dsData.Tables.Add(dtData)
            dsData.Tables(1).TableName = "Table"
            'ExportTemplate("Attendance\Import\Template_importTimesheet_CTT.xls", _
            '                          dsData, dtvariable, _
            '                          "Template_importTimesheet_CTT" & Format(Date.Now, "yyyyMMdd"))
            ExportTemplate("Attendance\Import\Template_importTimesheet_CTT.xls", dsData, dtvariable, "Template_importTimesheet_CTT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Time_TimeSheet_Rice()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.P_EXPORT_TYPE = 4
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            obj.ORG_ID = 0
            obj.IS_DISSOLVE = 0
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table1"
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(System.String))
            dtvariable.Columns.Add("D2", GetType(System.String))
            dtvariable.Columns.Add("D3", GetType(System.String))
            dtvariable.Columns.Add("D4", GetType(System.String))
            dtvariable.Columns.Add("D5", GetType(System.String))
            dtvariable.Columns.Add("D6", GetType(System.String))
            dtvariable.Columns.Add("D7", GetType(System.String))
            dtvariable.Columns.Add("D8", GetType(System.String))
            dtvariable.Columns.Add("D9", GetType(System.String))
            dtvariable.Columns.Add("D10", GetType(System.String))

            dtvariable.Columns.Add("D11", GetType(System.String))
            dtvariable.Columns.Add("D12", GetType(System.String))
            dtvariable.Columns.Add("D13", GetType(System.String))
            dtvariable.Columns.Add("D14", GetType(System.String))
            dtvariable.Columns.Add("D15", GetType(System.String))
            dtvariable.Columns.Add("D16", GetType(System.String))
            dtvariable.Columns.Add("D17", GetType(System.String))
            dtvariable.Columns.Add("D18", GetType(System.String))
            dtvariable.Columns.Add("D19", GetType(System.String))
            dtvariable.Columns.Add("D20", GetType(System.String))

            dtvariable.Columns.Add("D21", GetType(System.String))
            dtvariable.Columns.Add("D22", GetType(System.String))
            dtvariable.Columns.Add("D23", GetType(System.String))
            dtvariable.Columns.Add("D24", GetType(System.String))
            dtvariable.Columns.Add("D25", GetType(System.String))
            dtvariable.Columns.Add("D26", GetType(System.String))
            dtvariable.Columns.Add("D27", GetType(System.String))
            dtvariable.Columns.Add("D28", GetType(System.String))
            dtvariable.Columns.Add("D29", GetType(System.String))
            dtvariable.Columns.Add("D30", GetType(System.String))
            dtvariable.Columns.Add("D31", GetType(System.String))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = lsData.START_DATE
            Dim row = dtvariable.NewRow
            While dDay <= lsData.END_DATE
                row("D" & dDay.Value.Day) = dDay.Value.ToString("dd") & "/" & dDay.Value.ToString("MM")
                dDay = dDay.Value.AddDays(1)
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "Variable"
            Dim dtData = CType(Session("EXPORTIMESHEETRICE"), DataTable)
            dtData = dtData.Copy
            dsData.Tables.Add(dtData)
            dsData.Tables(1).TableName = "Table"
            ExportTemplate("Attendance\Import\Template_importTimesheet_Rice.xls", _
                                      dsData, dtvariable, _
                                      "Template_importTimesheet_Rice" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Time_TimeSheet_OT()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.P_EXPORT_TYPE = 4
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            obj.ORG_ID = 0
            obj.IS_DISSOLVE = 0
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table1"
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(System.String))
            dtvariable.Columns.Add("D2", GetType(System.String))
            dtvariable.Columns.Add("D3", GetType(System.String))
            dtvariable.Columns.Add("D4", GetType(System.String))
            dtvariable.Columns.Add("D5", GetType(System.String))
            dtvariable.Columns.Add("D6", GetType(System.String))
            dtvariable.Columns.Add("D7", GetType(System.String))
            dtvariable.Columns.Add("D8", GetType(System.String))
            dtvariable.Columns.Add("D9", GetType(System.String))
            dtvariable.Columns.Add("D10", GetType(System.String))

            dtvariable.Columns.Add("D11", GetType(System.String))
            dtvariable.Columns.Add("D12", GetType(System.String))
            dtvariable.Columns.Add("D13", GetType(System.String))
            dtvariable.Columns.Add("D14", GetType(System.String))
            dtvariable.Columns.Add("D15", GetType(System.String))
            dtvariable.Columns.Add("D16", GetType(System.String))
            dtvariable.Columns.Add("D17", GetType(System.String))
            dtvariable.Columns.Add("D18", GetType(System.String))
            dtvariable.Columns.Add("D19", GetType(System.String))
            dtvariable.Columns.Add("D20", GetType(System.String))

            dtvariable.Columns.Add("D21", GetType(System.String))
            dtvariable.Columns.Add("D22", GetType(System.String))
            dtvariable.Columns.Add("D23", GetType(System.String))
            dtvariable.Columns.Add("D24", GetType(System.String))
            dtvariable.Columns.Add("D25", GetType(System.String))
            dtvariable.Columns.Add("D26", GetType(System.String))
            dtvariable.Columns.Add("D27", GetType(System.String))
            dtvariable.Columns.Add("D28", GetType(System.String))
            dtvariable.Columns.Add("D29", GetType(System.String))
            dtvariable.Columns.Add("D30", GetType(System.String))
            dtvariable.Columns.Add("D31", GetType(System.String))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = lsData.START_DATE
            Dim row = dtvariable.NewRow
            While dDay <= lsData.END_DATE
                row("D" & dDay.Value.Day) = dDay.Value.ToString("dd") & "/" & dDay.Value.ToString("MM")
                dDay = dDay.Value.AddDays(1)
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "Variable"
            Dim dtData = CType(Session("EXPORTIMESHEETOT"), DataTable)
            dtData = dtData.Copy
            dsData.Tables.Add(dtData)
            dsData.Tables(1).TableName = "Table"
            ExportTemplate("Attendance\Import\Template_importTimesheet_OT.xls", _
                                      dsData, dtvariable, _
                                      "Template_importTimesheet_OT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Timesheet_machineExport()
        Try
            ExportTemplate("Attendance\Import\Template_GiaiTrinhNgayCong.xlsx",
                                      New DataSet(), Nothing,
                                      "Template_GiaiTrinhNgayCong" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WorkShiftImport()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 6
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(Date))
            dtvariable.Columns.Add("D2", GetType(Date))
            dtvariable.Columns.Add("D3", GetType(Date))
            dtvariable.Columns.Add("D4", GetType(Date))
            dtvariable.Columns.Add("D5", GetType(Date))
            dtvariable.Columns.Add("D6", GetType(Date))
            dtvariable.Columns.Add("D7", GetType(Date))
            dtvariable.Columns.Add("D8", GetType(Date))
            dtvariable.Columns.Add("D9", GetType(Date))
            dtvariable.Columns.Add("D10", GetType(Date))

            dtvariable.Columns.Add("D11", GetType(Date))
            dtvariable.Columns.Add("D12", GetType(Date))
            dtvariable.Columns.Add("D13", GetType(Date))
            dtvariable.Columns.Add("D14", GetType(Date))
            dtvariable.Columns.Add("D15", GetType(Date))
            dtvariable.Columns.Add("D16", GetType(Date))
            dtvariable.Columns.Add("D17", GetType(Date))
            dtvariable.Columns.Add("D18", GetType(Date))
            dtvariable.Columns.Add("D19", GetType(Date))
            dtvariable.Columns.Add("D20", GetType(Date))

            dtvariable.Columns.Add("D21", GetType(Date))
            dtvariable.Columns.Add("D22", GetType(Date))
            dtvariable.Columns.Add("D23", GetType(Date))
            dtvariable.Columns.Add("D24", GetType(Date))
            dtvariable.Columns.Add("D25", GetType(Date))
            dtvariable.Columns.Add("D26", GetType(Date))
            dtvariable.Columns.Add("D27", GetType(Date))
            dtvariable.Columns.Add("D28", GetType(Date))
            dtvariable.Columns.Add("D29", GetType(Date))
            dtvariable.Columns.Add("D30", GetType(Date))
            dtvariable.Columns.Add("D31", GetType(Date))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = lsData.START_DATE
            Dim row = dtvariable.NewRow
            Dim i As Integer = 1
            While dDay <= lsData.END_DATE
                row("D" & i) = dDay.Value
                dDay = dDay.Value.AddDays(1)
                i = i + 1
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "Variable"
            dsData.Tables.Add(dtvariable)
            ExportTemplate("Attendance\Import\Template_ImportShift.xls", _
                                      dsData, Nothing, _
                                      "Template_ImportShift" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportDMVS()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 2
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            ExportTemplate("Attendance\Import\Template_ImportDMVS.xls", _
                                      dsData, Nothing, _
                                      "Template_ImportDMVS" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportOT()
        Try
            'Dim rep As New Attendance.AttendanceRepository
            'Dim org_id = Decimal.Parse(Request.Params("orgid"))
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            'Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            'obj.ORG_ID = org_id
            'obj.IS_DISSOLVE = is_disolve
            'obj.P_EXPORT_TYPE = 2
            'If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
            '    obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            'End If
            'Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            'dsData.Tables(0).TableName = "Table"

            ExportTemplate("Attendance\Import\Template_ImportOT.xls", _
                                      New DataSet, Nothing, _
                                      "Template_ImportOT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_DeclareOT()
        Try
            'Dim rep As New Attendance.AttendanceRepository
            'Dim org_id = Decimal.Parse(Request.Params("orgid"))
            'Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            'obj.ORG_ID = org_id
            'obj.IS_DISSOLVE = True
            'obj.P_EXPORT_TYPE = 2
            'If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
            '    obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            'End If
            'Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            'dsData.Tables(0).TableName = "Table"
            Dim dsData As New DataSet
            ExportTemplate("Attendance\Import\Template_DeclareOT.xls", _
                                      dsData, Nothing, _
                                      "Template_DeclareOT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportShift_error()
        Try
            Dim dsData As DataSet = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_ImportShift_error.xls", _
                                      dsData, Nothing, _
                                      "Template_ImportShift_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Time_TimeSheetCCT_Error()
        Try
            Dim dsData As DataSet = Session("EXPORTREPORT")
            Dim rep As New Attendance.AttendanceRepository
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.P_EXPORT_TYPE = 4
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            If Not String.IsNullOrEmpty(Request.Params("orgid")) Then
                obj.ORG_ID = Decimal.Parse(Request.Params("orgid"))
            Else
                obj.ORG_ID = 0
            End If
            obj.IS_DISSOLVE = 0
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(System.String))
            dtvariable.Columns.Add("D2", GetType(System.String))
            dtvariable.Columns.Add("D3", GetType(System.String))
            dtvariable.Columns.Add("D4", GetType(System.String))
            dtvariable.Columns.Add("D5", GetType(System.String))
            dtvariable.Columns.Add("D6", GetType(System.String))
            dtvariable.Columns.Add("D7", GetType(System.String))
            dtvariable.Columns.Add("D8", GetType(System.String))
            dtvariable.Columns.Add("D9", GetType(System.String))
            dtvariable.Columns.Add("D10", GetType(System.String))

            dtvariable.Columns.Add("D11", GetType(System.String))
            dtvariable.Columns.Add("D12", GetType(System.String))
            dtvariable.Columns.Add("D13", GetType(System.String))
            dtvariable.Columns.Add("D14", GetType(System.String))
            dtvariable.Columns.Add("D15", GetType(System.String))
            dtvariable.Columns.Add("D16", GetType(System.String))
            dtvariable.Columns.Add("D17", GetType(System.String))
            dtvariable.Columns.Add("D18", GetType(System.String))
            dtvariable.Columns.Add("D19", GetType(System.String))
            dtvariable.Columns.Add("D20", GetType(System.String))

            dtvariable.Columns.Add("D21", GetType(System.String))
            dtvariable.Columns.Add("D22", GetType(System.String))
            dtvariable.Columns.Add("D23", GetType(System.String))
            dtvariable.Columns.Add("D24", GetType(System.String))
            dtvariable.Columns.Add("D25", GetType(System.String))
            dtvariable.Columns.Add("D26", GetType(System.String))
            dtvariable.Columns.Add("D27", GetType(System.String))
            dtvariable.Columns.Add("D28", GetType(System.String))
            dtvariable.Columns.Add("D29", GetType(System.String))
            dtvariable.Columns.Add("D30", GetType(System.String))
            dtvariable.Columns.Add("D31", GetType(System.String))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = lsData.START_DATE
            Dim row = dtvariable.NewRow
            While dDay <= lsData.END_DATE
                row("D" & dDay.Value.Day) = dDay.Value.ToString("dd") & "/" & dDay.Value.ToString("MM")
                dDay = dDay.Value.AddDays(1)
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "DATA_HEADER"
            dsData.Tables.Add(dtvariable)
            ExportTemplate("Attendance\Import\Template_importTimesheet_CTT_error.xls", _
                                      dsData, Nothing, _
                                      "Template_importTimesheet_CTT_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Time_TimeSheetCCT_Error1()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_ImportCTT_error.xls", _
                                      dtData, Nothing, _
                                      "Template_ImportCTT_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_importIO_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_importIO_error.xls", _
                                      dtData, Nothing, _
                                      "Template_importIO_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportDMVS_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_ImportDMVS_error.xls", _
                                      dtData, Nothing, _
                                      "Template_ImportDMVS_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportOT_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_ImportOT_error.xls", _
                                      dtData, Nothing, _
                                      "Template_ImportOT_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Template_GiaiTrinhNgayCong_error
    Private Sub Template_GiaiTrinhNgayCong_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_GiaiTrinhNgayCong_error.xlsx", _
                                      dtData, Nothing, _
                                      "Template_GiaiTrinhNgayCong_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_ImportSalary()
        Try
            Dim dtColName = Session("IMPORTSALARY_COLNAME")
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplateWithDataCol2("Payroll\Business\TEMP_IMPORT_SALARY.xlsx", dtData, dtColName, "TEMP_IMPORTSALARY" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_ImportThuongHQCV()
        Try
            Dim dtColName = Session("IMPORTSALARY_COLNAME")
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplateWithDataCol("Payroll\Business\TEMP_IMPORT_SALARY.xlsx", dtData, dtColName, "TEMP_IMPORTTHUONGHQCV" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_ImportQuyThuongHQCV()
        Try
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplate("Payroll\Business\TEMP_IMPORT_BONNUS.xlsx", dtData, "TEMP_IMPORTQUYTHUONGHQCV" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportQTThueTNCN()
        Try
            Dim dtColName = Session("IMPORTSALARY_COLNAME")
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplateWithDataCol1("Payroll\Business\TEMP_IMPORT_SALARY_TNCN.xlsx", dtData, dtColName, "TEMP_IMPORTTHUETNCN" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_ImportBonus()
        Try
            Dim dtColName = Session("IMPORTSALARY_COLNAME")
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplateWithDataCol1("Payroll\Business\TEMP_IMPORT_SALARY_BONUS.xlsx", dtData, dtColName, "TEMP_IMPORTBONUS" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportSalary_FundMapping()
        Try
            Dim dtColName = Session("IMPORTSALARY_COLNAME")
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplateWithDataColSP("Payroll\Business\TEMP_IMPORT_SALARY_FUND_MAPPING.xlsx", dtData, dtColName, "TEMP_IMPORT_SALARY_FUND_MAPPING" & Format(Date.Now, "yyyyMMdd"), 2)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ExportTemplateWithDataColSP(ByVal sReportFileName As String,
                                                ByVal dtDataValue As DataTable,
                                                ByVal dtColname As DataTable,
                                                ByVal filename As String,
                                                ByVal indexCol As Integer) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            dtDataValue.TableName = "DATA"

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cell As Cells = designer.Workbook.Worksheets(0).Cells
            Dim i As Integer = indexCol
            For Each dr As DataRow In dtColname.Rows
                cell(1, i).PutValue(dr("COLNAME"))
                cell(2, i).PutValue(dr("COLVAL"))
                cell(3, i).PutValue(dr("COLDATA"))
                i += 1
                cell.InsertColumn(i + 1)
            Next
            ' Xoa 2 cot thua cuoi cung
            cell.DeleteColumn(i + 1)
            cell.DeleteColumn(i)

            designer.SetDataSource(dtDataValue)
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Worksheets(0).AutoFitColumns()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                   ByVal dtData As DataTable,
                                   ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            dtData.TableName = "DATA"
            designer.SetDataSource(dtData)
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    'Private Sub Template_ImportSeniorityProcess()
    '    Try
    '        Using rep As New Payroll.PayrollRepository
    '            Dim org_id = Decimal.Parse(Request.Params("orgid"))
    '            Dim obj As New Payroll.PayrollBusiness.PASeniorityProcessDTO
    '            obj.ORG_ID = org_id
    '            obj.IS_DISSOLVE = Request.Params("IS_DISSOLVE")
    '            obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
    '            Dim dtData = rep.GetSeniorityProcessImport(obj)
    '            dtData.TableName = "DATA"
    '            ExportTemplate("Payroll\Business\TEMP_IMPORT_SENIORITY.xlsx", _
    '                                      dtData, Nothing, _
    '                                      "Template_SeniorityProcess_" & Format(Date.Now, "yyyyMMdd"))

    '        End Using
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub Template_ImportSeniorityProcess_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Payroll\Business\TEMP_IMPORT_SENIORITY_error.xlsx", _
                                      dtData, Nothing, _
                                      "Template_SeniorityProcess_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_DeclareOT_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_DeclareOT_error.xls", _
                                      dtData, Nothing, _
                                      "Template_DeclareOT_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportDM()
        Try
            Dim rep As New Insurance.InsuranceRepository
            Dim dsData As New DataSet
            Dim dtDataDis As DataTable = rep.GetHU_DISTRICT()
            Dim dtDataPro As DataTable = rep.GetHU_PROVINCE()
            Dim dtData As New DataTable
            dtData.Columns.Add("NAME")
            ' Tạo dữ liệu vlookup
            For i1 = 1 To 500
                Dim row = dtData.NewRow
                dtData.Rows.Add(row)
            Next
            dsData.Tables.Add(dtDataDis)
            dsData.Tables(0).TableName = "TbDis"
            dsData.Tables.Add(dtDataPro)
            dsData.Tables(1).TableName = "TbPro"
            dsData.Tables.Add(dtData)
            dsData.Tables(2).TableName = "DATA"

            ExportTemplate("Insurance\Import\Template_DMNoiKhamChuaBenh.xlsx", _
                                      dsData, Nothing, _
                                      "Template_DMNoiKhamChuaBenh" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportDMNK_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Insurance\Import\Template_DMNoiKhamChuaBenh_ERROR.xlsx", _
                                      dtData, Nothing, _
                                      "Import_DMNoiKhamChuaBenh_ERROR" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_InfoIns()
        Try
            Dim repIns As New Insurance.InsuranceRepository
            Dim repAtt As New Attendance.AttendanceRepository
            Dim dsData As New DataSet
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            ' lấy ra danh sách nơi khám chữa bệnh.
            Dim dtWhere As DataTable = repIns.GetINS_WHEREEXPORT().ToTable()
            Dim dtStatusSo As DataTable = repIns.GetStatuSo()
            Dim dtStatusHe As DataTable = repIns.GetStatuHE()

            ' lấy ra danh sách nhân viên trong 1 đơn vị được chọn.
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 5
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim dtEmpInOrg As DataSet = repAtt.GetDataFromOrg(obj)
            dtWhere.TableName = "NoiKham"
            dsData.Tables.Add(dtWhere)
            Dim dtEmp = dtEmpInOrg.Tables(0).Copy
            dtEmp.TableName = "EMP"
            dsData.Tables.Add(dtEmp)

            dtStatusSo.TableName = "SO"
            dsData.Tables.Add(dtStatusSo)

            dtStatusHe.TableName = "HE"
            dsData.Tables.Add(dtStatusHe)

            ExportTemplate("Insurance\Import\Template_ImportInfoIns.xlsx", _
                                      dsData, Nothing, _
                                      "Template_ImportInfoIns" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportInfoIns_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Insurance\Import\Template_ImportInfoIns_Error.xlsx", _
                                      dtData, Nothing, _
                                      "Template_ImportInfoIns_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TokhaiA01()
        Try
            Dim dtData = Nothing
            ExportTemplate("Insurance\Report\ToKhai.doc", _
                                     dtData, Nothing, _
                                     "ToKhai" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Register()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim dsData As New DataSet
            Dim dtDataTimeManual = rep.GetDataImportCO()
            dtDataTimeManual.TableName = "Manual"

            If dtDataTimeManual IsNot Nothing Then
                dsData.Tables.Add(dtDataTimeManual)
            End If

            ExportTemplate("Attendance\Import\AT_IMPORT_REGISTER_CO.xlsx", _
                                      dsData, Nothing, _
                                      "AT_IMPOERT_REGISTER_CO" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Register_Error()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim dtData = Session("EXPORTREPORT")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository

                ExportTemplate("Attendance\Import\AT_IMPORT_REGISTER_CO_Error.xlsx", _
                                          dtData, Nothing, _
                                          "AT_IMPORT_REGISTER_CO_Error" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HU_ChangeInfo()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository
                Dim ds = rep.GetChangeInfoImport(param)
                ds.Tables(0).TableName = "DATA"
                Dim i As Integer = 1
                For Each dt As DataTable In ds.Tables
                    If dt.TableName <> "DATA" Then
                        dt.TableName = "DATA" & i.ToString
                        i += 1
                    Else
                        ' Tạo dữ liệu vlookup
                        For i1 = 1 To 500
                            Dim row = dt.NewRow
                            dt.Rows.Add(row)
                        Next
                    End If
                Next
                ExportTemplate("Profile\Import\ChangeInfo.xls", _
                                          ds, Nothing, _
                                          "TemplateImport_" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HU_ChangeInfo_Error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Profile\Import\ChangeInfo_Error.xls", _
                                      dtData, Nothing, _
                                      "TemplateImport_Error_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HU_ANNUALLEAVE_PLANS_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_QLKeHoachNghiPN _error.xls", _
                                      dtData, Nothing, _
                                      "TemplateImport_Error_" & Format(Date.Now, "yyyyMMdd"))
            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_SingDefault()
        Try
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim rep As New Attendance.AttendanceRepository
            Dim repc As New Common.CommonRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            Dim dtDataShift As New DataTable
            obj.ORG_ID = org_id
            If Request.Params("IS_DISSOLVE") = "1" Then
                obj.IS_DISSOLVE = True
            End If
            obj.P_EXPORT_TYPE = 3
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            dtDataShift = rep.GetAT_ListShift()
            'check is root
            Dim ListComboData = New ComboBoxDataDTO
            ListComboData.GET_LIST_SHIFT = True
            rep.GetComboboxData(ListComboData)
            Dim list_id_shilf = ListComboData.LIST_LIST_SHIFT.Select(Function(n)
                                                                         Return n.ID
                                                                     End Function).ToList
            Dim listremove = dtDataShift.AsEnumerable.Where(Function(n)
                                                                Return Not list_id_shilf.Contains(n.Field(Of Decimal)("ID"))
                                                            End Function).ToList
            listremove.ForEach(Function(n)
                                   dtDataShift.Rows.Remove(n)
                                   Return True
                               End Function)
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            If dtDataShift IsNot Nothing Then
                dsData.Tables.Add(dtDataShift)
                dsData.Tables(1).TableName = "Table1"
            End If
            ExportTemplate("Attendance\Import\Import_SingDefault.xlsx", _
                                      dsData, Nothing, _
                                      "Import_SingDefault" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_SingDefault_Error()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim dtData = Session("EXPORTREPORT")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository

                ExportTemplate("Attendance\Import\Import_SingDefault_Error.xlsx", _
                                          dtData, Nothing, _
                                          "Import_SingDefault_Error_" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_DeclareTimeRice()
        Try
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            'Dim dtDataShift As New DataTable
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 7
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            'dtDataShift = rep.GetAT_ListShift()

            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            'If dtDataShift IsNot Nothing Then
            '    dsData.Tables.Add(dtDataShift)
            'dsData.Tables(1).TableName = "Table1"
            'End If
            ExportTemplate("Attendance\Import\Import_DeclareTimeRice.xlsx", _
                                      dsData, Nothing, _
                                      "Import_DeclareTimeRice" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_DeclareTimeRice_Error()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim dtData = Session("EXPORTREPORT")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository

                ExportTemplate("Attendance\Import\Import_DeclareTimeRice_Error.xlsx", _
                                          dtData, Nothing, _
                                          "Import_SingDefault_Error_" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportTimeSheetRice()
        Try
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            Dim dtDataPERIOD As New DataTable
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 7
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            dtDataPERIOD = rep.GetAT_PERIOD()

            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"

            If dtDataPERIOD IsNot Nothing Then
                dsData.Tables.Add(dtDataPERIOD)
                dsData.Tables(1).TableName = "Table1"
            End If

            ExportTemplate("Attendance\Import\Import_TimeSheetRice.xlsx", _
                                      dsData, Nothing, _
                                      "Import_TimeSheetRice" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_TimeSheetRice_Error()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim dtData = Session("EXPORTREPORT")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository

                ExportTemplate("Attendance\Import\Import_TimeSheetRice_Error.xlsx", _
                                          dtData, Nothing, _
                                          "Import_TimeSheetRice_Error" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportSwipeData()
        Try
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            'Dim rep As New Attendance.AttendanceRepository
            'Dim org_id = Decimal.Parse(Request.Params("orgid"))
            'Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            'Dim objter As New AT_TERMINALSDTO

            'Dim dtDatable As New DataTable("Table")
            'Dim dtDatableSTT As New DataTable("Table1")
            'dtDatableSTT.Columns.Add("STT", GetType(Integer))


            ''For index = 1 To 50
            ''    Dim workRow As DataRow = dtDatableSTT.NewRow()
            ''    workRow("STT") = index
            ''    dtDatableSTT.Rows.Add(workRow)
            ''Next


            'obj.ORG_ID = org_id
            'obj.IS_DISSOLVE = True
            'obj.P_EXPORT_TYPE = 3
            'If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
            '    obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            'End If

            'dtDatable = rep.GetTerminal(objter)

            'Dim dsData = New DataSet()

            'If dtDatableSTT IsNot Nothing Then
            '    dsData.Tables.Add(dtDatableSTT)
            'End If

            'If dtDatable IsNot Nothing Then
            '    dsData.Tables.Add(dtDatable)
            'End If

            Dim dsData = New DataSet()
            ExportTemplate("Attendance\Import\Import_SwipeData.xlsx", _
                                      dsData, Nothing, _
                                      "Import_SwipeData" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_SwipeData_Error()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim dtData = Session("EXPORTREPORT")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository

                ExportTemplate("Attendance\Import\Import_SwipeData_Error.xlsx", _
                                          dtData, Nothing, _
                                          "Import_SwipeData_Error" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IMPORT_SALARYPLANNING()
        Try
            Dim rep As New Payroll.PayrollRepository
            Dim dsData As New DataSet
            Dim org_id As Decimal = Request.Params("ORG_ID")
            dsData = rep.GetSalaryPlanningImport(org_id)
            dsData.Tables(0).TableName = "ORG"
            dsData.Tables(1).TableName = "TITLE"
            ExportTemplate("Payroll\Import\Template_SALARY_PLANNING.xls", _
                                      dsData, Nothing, _
                                      "Template_SALARY_PLANNING" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IMPORT_SALARYPLANNING_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Payroll\Import\Template_SALARY_PLANNING_error.xls", _
                                      dtData, Nothing, _
                                      "Template_SALARY_PLANNING_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_DeclareEntitlement()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 3
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            ExportTemplate("Attendance\Import\Template_DeclareEntitlement.xls", _
                                      dsData, Nothing, _
                                      "Template_DeclareOT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_DeclareEntitlement_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_DeclareEntitlement_error.xls", _
                                      dtData, Nothing, _
                                      "Template_DeclareEntitlement_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_INS_SUNCARE()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim repIns As New Insurance.InsuranceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            Dim dtDataCost As New DataTable
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 8
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "EMP"
            dtDataCost = repIns.GetLevelImport()
            dtDataCost.TableName = "Cost"
            dsData.Tables.Add(dtDataCost)

            ExportTemplate("Insurance\Import\Import_INS_SUNCARE.xls", _
                                      dsData, Nothing, _
                                      "Template_DeclareOT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_INS_SUNCARE_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Insurance\Import\Import_INS_SUNCARE_error.xlsx", _
                                      dtData, Nothing, _
                                      "Template_DeclareEntitlement_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Common"

    Public Function ExportTemplateWithDataCol(ByVal sReportFileName As String,
                                                    ByVal dtDataValue As DataTable,
                                                    ByVal dtColname As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            dtDataValue.TableName = "DATA"

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cell As Cells = designer.Workbook.Worksheets(0).Cells
            Dim st As New Style
            st.Number = 3
            Dim i As Integer = 4
            For Each dr As DataRow In dtColname.Rows
                cell(1, i).PutValue(dr("COLNAME"))
                cell(2, i).PutValue(dr("COLVAL"))
                cell(3, i).PutValue(dr("COLDATA"))
                cell(3, i).SetStyle(st)
                i += 1
                cell.InsertColumn(i + 1)
            Next
            ' Xoa 2 cot thua cuoi cung
            cell.DeleteColumn(i + 1)
            cell.DeleteColumn(i)

            designer.SetDataSource(dtDataValue)
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Worksheets(0).AutoFitColumns()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dtData As DataTable,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dtData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


#End Region

    Private Sub Template_ExportProgramDeclare()
        Try
            Dim rep As New Recruitment.RecruitmentStoreProcedure
            Try
                Dim configPath As String = Server.MapPath("ReportTemplates\Recruitment\Import\Template_import_kqtuyendung.xls")
                Dim dsData As DataSet = rep.GET_DECLARE_PROGRAM(Session("PROGRAMID"))
                dsData.Tables(0).TableName = "Table"
                ExportTemplate("Recruitment\Import\Template_import_kqtuyendung.xls",
                                      dsData, Nothing, "Template_import_kqtuyendung" & Format(Date.Now, "yyyyMMdd"))
                Session.Remove("PROGRAMID")
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Form_Suggest_Intern()
        Try
            Dim rep As New Recruitment.RecruitmentRepository
            Dim dsData As DataSet = rep.FormSuggestIntern(Session("PROGRAMID"))
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            ExportTemplate("Recruitment\Report\AVNS-" + Session("SuggestIntern_Value") + "-TDNS.xls",
                                      dsData, Nothing,
                                      "AVNS-" + Session("SuggestIntern_Value") + "-TDNS" & Format(Date.Now, "yyyyMMdd"))
            Session.Remove("PROGRAMID")
            Session.Remove("SuggestIntern_Value")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ExportTemplateWithDataCol1(ByVal sReportFileName As String,
                                                    ByVal dtDataValue As DataTable,
                                                    ByVal dtColname As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            dtDataValue.TableName = "DATA"

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cell As Cells = designer.Workbook.Worksheets(0).Cells
            Dim st As New Style
            st.Number = 4
            Dim i As Integer = 5
            For Each dr As DataRow In dtColname.Rows
                cell(1, i).PutValue(dr("COLNAME"))
                cell(2, i).PutValue(dr("COLVAL"))
                cell(3, i).PutValue(dr("COLDATA"))
                cell(3, i).SetStyle(st)
                i += 1
                cell.InsertColumn(i + 1)
            Next
            ' Xoa 2 cot thua cuoi cung
            cell.DeleteColumn(i + 1)
            cell.DeleteColumn(i)

            designer.SetDataSource(dtDataValue)
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Worksheets(0).AutoFitColumns()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function ExportTemplateWithDataCol2(ByVal sReportFileName As String,
                                                    ByVal dtDataValue As DataTable,
                                                    ByVal dtColname As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            dtDataValue.TableName = "DATA"

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cell As Cells = designer.Workbook.Worksheets(0).Cells
            Dim st As New Style
            st.Number = 4
            Dim i As Integer = 5
            For Each dr As DataRow In dtColname.Rows
                cell(1, i).PutValue(dr("COLNAME"))
                cell(2, i).PutValue(dr("COLVAL"))
                cell(3, i).PutValue(dr("COLDATA"))
                cell(3, i).SetStyle(st)
                i += 1
                cell.InsertColumn(i + 1)
            Next
            ' Xoa 2 cot thua cuoi cung
            cell.DeleteColumn(i + 1)
            cell.DeleteColumn(i)

            designer.SetDataSource(dtDataValue)
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Worksheets(0).AutoFitColumns()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub Template_Allowance()
        Dim rep As New Recruitment.RecruitmentStoreProcedure
        Try
            Dim configPath As String = Server.MapPath("ReportTemplates\Payroll\Import\Allowance.xls")
            Dim dsData As DataSet = rep.GET_ALLOWANCE()
            dsData.Tables(0).TableName = "Table"
            ExportTemplate("Payroll\Import\Allowance.xls",
                                  dsData, Nothing, "Template_Allowance" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception

        End Try
    End Sub


End Class