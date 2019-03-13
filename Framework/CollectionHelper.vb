﻿Imports System.Reflection
Imports System.ComponentModel

Public Class CollectionHelper
    Implements IDisposable


    Public Function ConvertTo(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim table As DataTable = CreateTable(Of T)()
        Dim entityType As Type = GetType(T)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(entityType)

        For Each item As T In list
            Dim row As DataRow = table.NewRow()

            For Each prop As PropertyDescriptor In properties
                row(prop.Name) = prop.GetValue(item)
            Next prop

            table.Rows.Add(row)
        Next item

        Return table
    End Function

    Public Function ConvertTo(Of T)(ByVal rows As IList(Of DataRow)) As IList(Of T)
        Dim list As IList(Of T) = Nothing

        If rows IsNot Nothing Then
            list = New List(Of T)()

            For Each row As DataRow In rows
                Dim item As T = CreateItem(Of T)(row)
                list.Add(item)
            Next row
        End If

        Return list
    End Function

    Public Function ConvertTo(Of T)(ByVal table As DataTable) As IList(Of T)
        If table Is Nothing Then
            Return Nothing
        End If

        Dim rows As New List(Of DataRow)()

        For Each row As DataRow In table.Rows
            rows.Add(row)
        Next row

        Return ConvertTo(Of T)(rows)
    End Function

    Public Function CreateItem(Of T)(ByVal row As DataRow) As T
        Dim obj As T = Nothing
        If row IsNot Nothing Then
            obj = Activator.CreateInstance(Of T)()

            For Each column As DataColumn In row.Table.Columns
                Dim prop As PropertyInfo = obj.GetType().GetProperty(column.ColumnName)
                If prop IsNot Nothing Then
                    Try
                        Dim value As Object = row(column.ColumnName)
                        value = IIf(value Is DBNull.Value, Nothing, value)
                        If value IsNot Nothing Then
                            Select Case prop.PropertyType
                                Case GetType(Boolean), GetType(Boolean?)
                                    If value Is Nothing Then
                                        value = False
                                    End If
                                    value = IIf(value = -1, True, False)
                                Case GetType(Date), GetType(Date?)
                                    Date.TryParse(value, value)
                                Case GetType(DateTime), GetType(DateTime?)
                                    DateTime.TryParse(value, value)
                                Case GetType(Double), GetType(Double?)
                                    Double.TryParse(value, value)
                                Case GetType(Decimal), GetType(Decimal?)
                                    Decimal.TryParse(value, value)
                                Case GetType(Integer), GetType(Integer?)
                                    Integer.TryParse(value, value)
                            End Select
                        End If

                        prop.SetValue(obj, value, Nothing)
                    Catch
                        ' You can log something here
                        Throw
                    End Try
                End If

            Next column
        End If

        Return obj
    End Function

    Public Function CreateTable(Of T)() As DataTable
        Dim entityType As Type = GetType(T)
        Dim table As New DataTable(entityType.Name)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(entityType)

        For Each prop As PropertyDescriptor In properties
            table.Columns.Add(prop.Name, prop.PropertyType)
        Next prop

        Return table
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class