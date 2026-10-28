Imports System.Data.OleDb
Module Module1
    Public conn As OleDbConnection
    Public da As OleDbDataAdapter
    Public ds As DataSet
    Public cmd As OleDbCommand
    Public dr As OleDbDataReader
    Public re As New BindingSource
    Public byteimg As Byte
    Public controlenable As Boolean = False
    Public Sub koneksi()
        conn = New OleDbConnection("provider=microsoft.ace.oledb.12.0; data source=laundry.accdb")
        conn.Open()
    End Sub
End Module
