Imports System.Data.OleDb
Public Class formlogin
    Dim connString As String = "provider=microsoft.ace.oledb.12.0; data source=laundry.accdb;"
    Dim connection As New OleDbConnection(connString)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            connection.Open()

            Dim query As String = "SELECT role FROM tb_user WHERE username=? AND password=?"
            Dim command As New OleDbCommand(query, connection)
            command.Parameters.AddWithValue("username", TextBox1.Text)
            command.Parameters.AddWithValue("password", TextBox2.Text)

            Dim userRole As Object = command.ExecuteScalar()

            'If TextBox1.Text = "admin" And TextBox2.Text = "12345" Then
            '    mainmenuadmin.Show()
            '    Me.Hide()
            '    MsgBox("Welcome admin!", vbInformation, "success!")
            '    TextBox1.Text = ""
            '    TextBox2.Text = ""
            If userRole IsNot Nothing Then
                ' Buka formulir yang sesuai berdasarkan peran pengguna
                Select Case userRole.ToString()
                    Case "Chasier"
                        mainmenuchasier.Show()
                        Me.Hide()
                        MsgBox("Welcome chasier!", vbInformation, "success!")

                    Case "Owner"
                        formowner.Show()
                        Me.Hide()
                        MsgBox("Welcome Owner!", vbInformation, "success!")

                    Case "Admin"
                        mainmenuadmin.Show()
                        Me.Hide()
                        MsgBox("Welcome Admin!", vbInformation, "success!")

                        ' Tambahkan kasus lain sesuai kebutuhan
                    Case Else
                        MsgBox("invalid user role!", vbCritical, "sorry!")
                End Select
            Else
                MsgBox("Incorrect username or password!", vbCritical, "sorry!")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub formlogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
