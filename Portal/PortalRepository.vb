Imports Portal.PortalBusiness
Imports Common.CommonBusiness

Public Class PortalRepository
    Inherits PortalRepositoryBase

#Region "Event Information"
    ''' <summary>
    ''' Lấy thông tin sự kiện
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEventInformation() As EventDTO
        Using rep As New PortalBusinessClient
            Try
                Return rep.GetEventInformation(Nothing)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    ''' <summary>
    ''' Lấy danh sách thông tin sự kiện
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListEventInformation() As List(Of EventDTO)
        Using rep As New PortalBusinessClient
            Try
                Return rep.GetListEventInformation(Nothing).ToList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    ''' <summary>
    ''' Thêm mới thông tin sự kiện
    ''' </summary>
    ''' <param name="_event"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertEventInformation(ByVal _event As EventDTO) As Boolean
        Using rep As New PortalBusinessClient
            Try
                Return rep.InsertEventInformation(_event, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    ''' <summary>
    ''' Sửa thông tin sự kiện
    ''' </summary>
    ''' <param name="_event"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateEventInformation(ByVal _event As EventDTO) As Boolean
        Using rep As New PortalBusinessClient
            Try
                Return rep.UpdateEventInformation(_event, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    ''' <summary>
    ''' Xóa thông tin sự kiện
    ''' </summary>
    ''' <param name="_listId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEventInformation(ByVal _listId As List(Of Decimal)) As Boolean
        Using rep As New PortalBusinessClient
            Try
                Return rep.DeleteEventInformation(_listId, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    ''' <summary>
    ''' Mở hiện sự kiện
    ''' </summary>
    ''' <param name="_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ActiveEventInformation(ByVal _id As Decimal) As Boolean
        Using rep As New PortalBusinessClient
            Try
                Return rep.ActiveEventInformation(_id, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

End Class
