Imports System.Data.OleDb
Public Class formuserdata
    Sub kosongkan()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        ComboBox1.Text = ""
        TextBox5.Clear()
        ComboBox2.Text = ""
    End Sub
    Sub databaru()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        ComboBox1.Text = ""
        ComboBox2.Text = ""
    End Sub
    Sub ketemu()
        On Error Resume Next
        TextBox2.Text = dr.Item(1)
        TextBox3.Text = dr.Item(2)
        TextBox4.Text = dr.Item(3)
        ComboBox1.Text = dr.Item(4)
        ComboBox2.Text = dr.Item(5)
    End Sub
    Sub carikode()
        cmd = New OleDbCommand("select * from tb_user where id ='" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub
    Sub tampilgrid()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_user ORDER BY id DESC", conn)
        ds = New DataSet
        da.Fill(ds, "tb_user")
        DataGridView1.DataSource = ds.Tables("tb_user")
        'responsive dg
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Sub refeshgrid()
        da = New OleDbDataAdapter("select * from tb_user", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tb_user")
        DataGridView1.DataSource = (ds.Tables("tb_user"))
    End Sub
    Sub kode()
        Call koneksi()
        cmd = New OleDbCommand("select * from tb_user where id in(select max(id) from tb_user)", conn)
        Dim urutkode As String
        Dim hitung As Long
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            urutkode = "USR" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(dr.GetString(0), 3) + 1
            urutkode = "USR" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        TextBox1.Text = urutkode
        TextBox2.Focus()
    End Sub
    Sub simpandata()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_outlet ", conn)
        ds = New DataSet
        da.Fill(ds)
        re.DataSource = ds
        re.DataMember = ds.Tables(0).ToString
    End Sub
    Sub itemcombo()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_outlet ", conn)
        ds = New DataSet
        da.Fill(ds)
        re.DataSource = ds
        re.DataMember = ds.Tables(0).ToString
        Dim a As DataRow
        ComboBox1.Items.Clear()
        For Each a In ds.Tables(0).Rows
            ComboBox1.Items.Add(a.Item(0))
        Next a
    End Sub
    Sub KetemuUser()
        Call koneksi()
        da = New OleDbDataAdapter("SELECT * FROM where tb_user username LIKE '%" & formlogin.TextBox1.Text & "%'", conn)
        ds = New DataSet
        TextBox7.Text = formlogin.TextBox1.Text

    End Sub
    Sub ValidateUser()
        Dim username As String = TextBox7.Text
        Dim connectionString As String = "provider=microsoft.ace.oledb.12.0; data source=laundry.accdb"
        Dim connection As New OleDbConnection(connectionString)

        Try
            connection.Open()
            Dim query As String = "SELECT * FROM tb_user WHERE username = ?"
            Dim cmd As New OleDbCommand(query, connection)
            cmd.Parameters.AddWithValue("@username", username)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                TextBox7.Text = reader("username").ToString()
                MessageBox.Show("User not found.")
            End If
            reader.Close()
            connection.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Sub
    Private Sub formuseradmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Call databaru()
        Call koneksi()
        Call tampilgrid()
        Call kosongkan()
        Call kode()
        Call itemcombo()
        Call simpandata()
        Call ValidateUser()
        Call KetemuUser()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MsgBox("Data is incomplete", vbInformation, "Sorry!")
            Exit Sub
        Else
            Call carikode()
            If Not dr.HasRows Then
                Dim simpan As String = "insert into tb_user values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "')"
                cmd = New OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                Call refeshgrid()
                Call kosongkan()
                Call tampilgrid()
                Call kode()
            End If
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MsgBox("Data is incomplete", vbInformation, "Sorry!")
        Else
            Try
                Call koneksi()
                Dim EDIT As String = "UPDATE tb_user SET name=?, username=?, [password]=?, id_outlet=?, role=? WHERE id=?"
                cmd = New OleDbCommand(EDIT, conn)
                cmd.Parameters.AddWithValue("@name", TextBox2.Text)
                cmd.Parameters.AddWithValue("@username", TextBox3.Text)
                cmd.Parameters.AddWithValue("@password", TextBox4.Text)
                cmd.Parameters.AddWithValue("@id_outlet", ComboBox1.Text)
                cmd.Parameters.AddWithValue("@role", ComboBox2.Text)
                cmd.Parameters.AddWithValue("@id", TextBox1.Text)
                cmd.ExecuteNonQuery()
                MsgBox("successful data change!", vbInformation, "Information!")
                Call refeshgrid()
                Call kosongkan()
                Call tampilgrid()
                Call kode()
            Catch ex As OleDbException
                MsgBox("OleDbException: " & ex.Message, vbExclamation, "Error")
            Catch ex As Exception
                MsgBox("Error: " & ex.Message, vbExclamation, "Error")
            Finally
                conn.Close()
            End Try
        End If
        'If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
        '    MsgBox("Data Belum Lengkap..!", vbInformation, "GAGAL!")
        'Else
        '    Call koneksi()
        '    Dim EDIT As String = "update tb_user set name='" & TextBox2.Text & "',username='" & TextBox3.Text & "',password='" & TextBox4.Text & "',id_outlet='" & ComboBox1.Text & "',role='" & ComboBox2.Text & "' where id='" & TextBox1.Text & "'"
        '    cmd = New OleDbCommand(EDIT, conn)
        '    cmd.ExecuteNonQuery()
        '    MsgBox("Data Berhasil Diedit!", vbInformation, "SUKSES!")
        'End If
        'Call refeshgrid()
        'Call kosongkan()
        'Call tampilgrid()
        'Call kode()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        mainmenuadmin.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        formcustomer.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        formdataoutlet.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        formpackagedata.Show()
        Me.Hide()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If TextBox1.Text = "" Then
            MsgBox("code must be filled in", vbInformation, "sorry!")
            TextBox1.Text = Focus()
            Exit Sub
        End If
        Call carikode()
        If Not dr.HasRows Then
            MsgBox("code not found", vbInformation, "sorry!")
            TextBox1.Focus()
            Exit Sub
        End If
        If MessageBox.Show("are you sure it will be deleted?", "confirm!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Dim hapus As String = "delete from tb_user where id='" & TextBox1.Text & "'"
            cmd = New OleDbCommand(hapus, conn)
            cmd.ExecuteNonQuery()
            Call refeshgrid()
            Call kosongkan()
            Call tampilgrid()
            Call kosongkan()
            Call kode()
        End If
    End Sub
    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        On Error Resume Next
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Call carikode()
        If dr.HasRows Then
            Call ketemu()
        End If
    End Sub
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call carikode()
            If dr.HasRows Then
            Else
                Call databaru()
            End If
        End If
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        da = New OleDbDataAdapter("select * from tb_member where name like'%" & TextBox6.Text & "%'or phone like '%" & TextBox6.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub
    Private Sub DataGridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Dim strrownumber As String = (e.RowIndex + 1).ToString
        Dim size As SizeF = e.Graphics.MeasureString(strrownumber, Me.Font)
        If DataGridView1.RowHeadersWidth < CInt((size.Width + 20)) Then
            DataGridView1.RowHeadersWidth = CInt((size.Width + 20))
        End If
        Dim b As Brush = SystemBrushes.ControlText
        e.Graphics.DrawString(strrownumber, Me.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("Are you sure you want to log-out??", "confirm!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            formlogin.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call koneksi()
        cmd = New OleDb.OleDbCommand("select*from tb_outlet where id='" & ComboBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        TextBox5.Text = dr.Item(1)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Call kosongkan()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        mainmenuadmin.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        formcustomer.kosongkan()
        formcustomer.kode()
        formcustomer.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        formdataoutlet.kosongkan()
        formdataoutlet.kode()
        formdataoutlet.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        formpackagedata.kosongkan()
        formpackagedata.kode()
        formpackagedata.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        formtransaksiterima.kosongkan()
        formtransaksiterima.kode()
        formtransaksiterima.Show()
        Me.Hide()
    End Sub

    Private Sub Button9_Click_1(sender As Object, e As EventArgs) Handles Button9.Click
        If MessageBox.Show("Are you sure you want to log-out??", "confirm!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            formlogin.TextBox1.Clear()
            formlogin.TextBox2.Clear()
            formlogin.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        formtransaksiambil.kosongkan()
        formtransaksiambil.kodetrans()
        formtransaksiambil.Show()
        Me.Hide()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        formrekapmasuk.tampilgridtrans()
        formrekapmasuk.refeshgridtrans()
        formrekapmasuk.Show()
        Me.Hide()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        formrekapambil.tampilgridtrans()
        formrekapambil.refeshgridtrans()
        formrekapambil.Show()
        Me.Hide()
    End Sub
End Class