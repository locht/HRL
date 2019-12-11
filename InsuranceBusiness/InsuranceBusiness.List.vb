Imports InsuranceBusiness.ServiceContracts
Imports InsuranceDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace InsuranceBusiness.ServiceImplementations
    Partial Public Class InsuranceBusiness
        Implements IInsuranceBusiness

#Region "Danh mục đơn vị bảo hiểm"
        Public Function GetInsListInsurance(Optional ByVal Is_Full As Boolean = False) As DataTable _
            Implements ServiceContracts.IInsuranceBusiness.GetInsListInsurance
            Try

                Dim rep As New DataAccess.QueryData
                Dim dtResult As Object = rep.ExecuteStore("PKG_INS_LIST.SPS_INS_LIST_INSURANCE", New With {.P_USERNAME = "",
                                                                                                         .P_LOADFULL = If(Is_Full, "1", "0"),
                                                                                                           .P_CUR = OUT_CURSOR})
                Return dtResult
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function GetInsListInsuranceByUsername(ByVal User_Name As String, Optional ByVal Is_Full As Boolean = False) As DataTable _
            Implements ServiceContracts.IInsuranceBusiness.GetInsListInsuranceByUsername
            Try

                Dim rep As New DataAccess.QueryData
                Dim dtResult As Object = rep.ExecuteStore("PKG_INS_LIST.SPS_INS_LIST_INSURANCE", New With {.P_USERNAME = User_Name,
                                                                                                           .P_LOADFULL = If(Is_Full, "1", "0"),
                                                                                                           .P_CUR = OUT_CURSOR})
                Return dtResult
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function UpdateInsListInsurance(ByVal username As String, ByVal id As Double? _
                                            , ByVal code As String _
                                            , ByVal name As String _
                                            , ByVal address As String _
                                            , ByVal phone_number As String) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.UpdateInsListInsurance
            Try
                Dim rep As New DataAccess.QueryData
                Dim objU As Object = rep.ExecuteStore("PKG_INS_LIST.SPU_INS_LIST_INSURANCE", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                    , .P_CODE = code _
                                                    , .P_NAME = name _
                                                    , .P_ADDRESS = address _
                                                    , .P_PHONE_NUMBER = phone_number})
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function DeleteInsListInsurance(ByVal id As Double?) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.DeleteInsListInsurance
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_LIST.SPD_INS_LIST_INSURANCE",
                                                      New With {.P_ID = IIf(id Is Nothing, System.DBNull.Value, id)})
                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function
#End Region
#Region "Danh mục nơi khám chữa bệnh"
        Public Function GetINS_WHEREHEALTH(ByVal _filter As INS_WHEREHEALTHDTO,
                                          Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_WHEREHEALTHDTO) _
                                     Implements ServiceContracts.IInsuranceBusiness.GetINS_WHEREHEALTH
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_WHEREHEALTH(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetINS_WHEREEXPORT() As List(Of INS_WHEREHEALTHDTO) _
                                   Implements ServiceContracts.IInsuranceBusiness.GetINS_WHEREEXPORT
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_WHEREEXPORT()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_PROVINCE(Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal Sorts As String = "NAME_VN desc") As DataTable _
                                     Implements ServiceContracts.IInsuranceBusiness.GetHU_PROVINCE
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetHU_PROVINCE(PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetStatuSo() As DataTable _
                                     Implements ServiceContracts.IInsuranceBusiness.GetStatuSo
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetStatuSo()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetStatuHE() As DataTable _
                                     Implements ServiceContracts.IInsuranceBusiness.GetStatuHE
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetStatuHE()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_DISTRICT(Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal Sorts As String = "NAME_VN desc") As DataTable _
                                     Implements ServiceContracts.IInsuranceBusiness.GetHU_DISTRICT
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetHU_DISTRICT(PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertINS_WHEREHEALTH(ByVal objDMVS As INS_WHEREHEALTHDTO,
                                              ByVal log As UserLog,
                                              ByRef gID As Decimal) As Boolean _
                                          Implements ServiceContracts.IInsuranceBusiness.InsertINS_WHEREHEALTH
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertINS_WHEREHEALTH(objDMVS, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateINS_WHEREHEALTH(ByVal objDMVS As INS_WHEREHEALTHDTO) As Boolean _
            Implements ServiceContracts.IInsuranceBusiness.ValidateINS_WHEREHEALTH
            Using rep As New InsuranceRepository
                Try

                    Return rep.ValidateINS_WHEREHEALTH(objDMVS)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyINS_WHEREHEALTH(ByVal objDMVS As INS_WHEREHEALTHDTO,
                                              ByVal log As UserLog,
                                              ByRef gID As Decimal) As Boolean _
                                          Implements ServiceContracts.IInsuranceBusiness.ModifyINS_WHEREHEALTH
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyINS_WHEREHEALTH(objDMVS, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveINS_WHEREHEALTH(ByVal lstID As List(Of Decimal),
                                              ByVal log As UserLog,
                                              ByVal bActive As String) As Boolean _
                                          Implements ServiceContracts.IInsuranceBusiness.ActiveINS_WHEREHEALTH
            Using rep As New InsuranceRepository
                Try

                    Return rep.ActiveINS_WHEREHEALTH(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteINS_WHEREHEALTH(ByVal lstID As List(Of Decimal)) As Boolean _
            Implements ServiceContracts.IInsuranceBusiness.DeleteINS_WHEREHEALTH
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteINS_WHEREHEALTH(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDistrictByIDPro(ByVal province_ID As Decimal) As DataTable _
            Implements ServiceContracts.IInsuranceBusiness.GetDistrictByIDPro
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetDistrictByIDPro(province_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


#End Region

#Region "Chế độ bảo hiểm"
        Public Function GetEntitledType(ByVal _filter As INS_ENTITLED_TYPE_DTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_ENTITLED_TYPE_DTO) Implements ServiceContracts.IInsuranceBusiness.GetEntitledType
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetEntitledType(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertEntitledType(ByVal objTitle As INS_ENTITLED_TYPE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertEntitledType
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertEntitledType(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyEntitledType(ByVal objTitle As INS_ENTITLED_TYPE_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyEntitledType
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyEntitledType(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateEntitledType(ByVal _validate As INS_ENTITLED_TYPE_DTO) Implements ServiceContracts.IInsuranceBusiness.ValidateEntitledType
            Using rep As New InsuranceRepository
                Try

                    Return rep.ValidateEntitledType(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveEntitledType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IInsuranceBusiness.ActiveEntitledType
            Using rep As New InsuranceRepository
                Try

                    Return rep.ActiveEntitledType(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteEntitledType(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteEntitledType
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteEntitledType(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Danh muc bien dong bao hiem"

        Public Function GetInsChangeType(ByVal _filter As INS_CHANGE_TYPEDTO,
                                        Optional ByRef PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_CHANGE_TYPEDTO) Implements ServiceContracts.IInsuranceBusiness.GetInsChangeType
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetInsChangeType(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertInsChangeType(ByVal objInsChangeType As INS_CHANGE_TYPEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertInsChangeType
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertInsChangeType(objInsChangeType, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateInsChangeType(ByVal objInsChangeType As INS_CHANGE_TYPEDTO) As Boolean Implements ServiceContracts.IInsuranceBusiness.ValidateInsChangeType
            Using rep As New InsuranceRepository
                Try

                    Return rep.ValidateInsChangeType(objInsChangeType)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyInsChangeType(ByVal objInsChangeType As INS_CHANGE_TYPEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyInsChangeType
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyInsChangeType(objInsChangeType, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveInsChangeType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IInsuranceBusiness.ActiveInsChangeType
            Using rep As New InsuranceRepository
                Try

                    Return rep.ActiveInsChangeType(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteInsChangeType(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteInsChangeType
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteInsChangeType(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quy định đối tượng"
        Public Function GetSpecifiedObjects(ByVal _filter As INS_SPECIFIED_OBJECTS_DTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_SPECIFIED_OBJECTS_DTO) Implements ServiceContracts.IInsuranceBusiness.GetSpecifiedObjects
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetSpecifiedObjects(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertSpecifiedObjects(ByVal objTitle As INS_SPECIFIED_OBJECTS_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertSpecifiedObjects
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertSpecifiedObjects(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifySpecifiedObjects(ByVal objTitle As INS_SPECIFIED_OBJECTS_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifySpecifiedObjects
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifySpecifiedObjects(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function ActiveSpecifiedObjects(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ActiveSpecifiedObjects
            Using rep As New InsuranceRepository
                Try

                    Return rep.ActiveSpecifiedObjects(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteSpecifiedObjects(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteSpecifiedObjects
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteSpecifiedObjects(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ObjectPayInsurrance(ByVal lstID As List(Of String), ByVal objName As String) As Boolean Implements ServiceContracts.IInsuranceBusiness.ObjectPayInsurrance
            Using rep As New InsuranceRepository
                Try

                    Return rep.ObjectPayInsurrance(lstID, objName)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Danh mục vùng đóng bảo hiểm"
        Public Function GetINS_REGION(ByVal _filter As INS_REGION_DTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_REGION_DTO) Implements ServiceContracts.IInsuranceBusiness.GetINS_REGION
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_REGION(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertINS_REGION(ByVal objDMVS As INS_REGION_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertINS_REGION
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertINS_REGION(objDMVS, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateINS_REGION(ByVal objDMVS As INS_REGION_DTO) As Boolean Implements ServiceContracts.IInsuranceBusiness.ValidateINS_REGION
            Using rep As New InsuranceRepository
                Try

                    Return rep.ValidateINS_REGION(objDMVS)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyINS_REGION(ByVal objDMVS As INS_REGION_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyINS_REGION
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyINS_REGION(objDMVS, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveINS_REGION(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IInsuranceBusiness.ActiveINS_REGION
            Using rep As New InsuranceRepository
                Try

                    Return rep.ActiveINS_REGION(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteINS_REGION(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteINS_REGION
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteINS_REGION(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Danh mục chi phí theo cấp"
        Public Function GetINS_COST_FOLLOW_LEVER(ByVal _filter As INS_COST_FOLLOW_LEVERDTO,
                                                Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_COST_FOLLOW_LEVERDTO) Implements ServiceContracts.IInsuranceBusiness.GetINS_COST_FOLLOW_LEVER
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_COST_FOLLOW_LEVER(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertINS_COST_FOLLOW_LEVER(ByVal objDMVS As INS_COST_FOLLOW_LEVERDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertINS_COST_FOLLOW_LEVER
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertINS_COST_FOLLOW_LEVER(objDMVS, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateINS_COST_FOLLOW_LEVER(ByVal objDMVS As INS_COST_FOLLOW_LEVERDTO) As Boolean Implements ServiceContracts.IInsuranceBusiness.ValidateINS_COST_FOLLOW_LEVER
            Using rep As New InsuranceRepository
                Try

                    Return rep.ValidateINS_COST_FOLLOW_LEVER(objDMVS)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyINS_COST_FOLLOW_LEVER(ByVal objDMVS As INS_COST_FOLLOW_LEVERDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyINS_COST_FOLLOW_LEVER
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyINS_COST_FOLLOW_LEVER(objDMVS, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveINS_COST_FOLLOW_LEVER(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IInsuranceBusiness.ActiveINS_COST_FOLLOW_LEVER
            Using rep As New InsuranceRepository
                Try

                    Return rep.ActiveINS_COST_FOLLOW_LEVER(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteINS_COST_FOLLOW_LEVER(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteINS_COST_FOLLOW_LEVER
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteINS_COST_FOLLOW_LEVER(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Danh mục chế độ hưởng bảo hiểm"
        Public Function GetINS_REGIMES(ByVal _filter As INS_GROUP_REGIMESDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "REGIMES_NAME desc") As List(Of INS_GROUP_REGIMESDTO) Implements ServiceContracts.IInsuranceBusiness.GetINS_REGIMES
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_REGIMES(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertINS_REGIMES(ByVal objDMVS As INS_GROUP_REGIMESDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertINS_REGIMES
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertINS_REGIMES(objDMVS, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function ModifyINS_REGIMES(ByVal objDMVS As INS_GROUP_REGIMESDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyINS_REGIMES
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyINS_REGIMES(objDMVS, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActiveINS_REGIMES(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IInsuranceBusiness.ActiveINS_REGIMES
            Using rep As New InsuranceRepository
                Try

                    Return rep.ActiveINS_REGIMES(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteINS_REGIMES(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteINS_REGIMES
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteINS_REGIMES(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "System"
        Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String _
           Implements ServiceContracts.IInsuranceBusiness.AutoGenCode
            Using rep As New InsuranceRepository
                Try

                    Dim lst = rep.AutoGenCode(firstChar, tableName, colName)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As InsuranceCommon.TABLE_NAME) As Boolean _
            Implements ServiceContracts.IInsuranceBusiness.CheckExistInDatabase
            Using rep As New InsuranceRepository
                Try

                    Dim lst = rep.CheckExistInDatabase(lstID, table)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Validate Combobox"
        Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean _
            Implements ServiceContracts.IInsuranceBusiness.ValidateCombobox
            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.ValidateCombobox(cbxData)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

    End Class
End Namespace
