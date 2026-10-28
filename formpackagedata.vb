Imports System.Data.OleDb
Public Class formpackagedata
    Sub kosongkan()
        ComboBox2.Text = ""
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox5.Clear()
    End Sub
    Sub databaru()
        ComboBox2.Text = ""
        TextBox2.Clear()
        TextBox3.Clear()
    End Sub
    Sub ketemu()
        On Error Resume Next
        ComboBox2.Text = dr.Item(1)
        TextBox2.Text = dr.Item(2)
        TextBox3.Text = dr.Item(3)
    End Sub
    Sub carikode()
        cmd = New OleDbCommand("select * from tb_paket where id ='" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub
    Sub tampilgrid()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_paket ORDER BY id DESC", conn)
        ds = New DataSet
        da.Fill(ds, "tb_paket")
        DataGridView1.DataSource = ds.Tables("tb_paket")
        'responsive dg
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Sub refeshgrid()
        da = New OleDbDataAdapter("select * from tb_paket", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tb_paket")
        DataGridView1.DataSource = (ds.Tables("tb_paket"))
    End Sub
    Sub kode()
        Call koneksi()
        cmd = New OleDbCommand("select * from tb_paket where id in(select max(id) from tb_paket)", conn)
        Dim urutkode As String
        Dim hitung As Long
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            urutkode = "PCK" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(dr.GetString(0), 3) + 1
            urutkode = "PCK" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        TextBox1.Text = urutkode
        TextBox2.Focus()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        formcustomer.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        formdataoutlet.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        formuserdata.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        mainmenuadmin.Show()
        Me.Hide()
    End Sub

    Private Sub formlaudryadmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Call databaru()
        Call koneksi()
        Call tampilgrid()
        Call kosongkan()
        Call kode()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If TextBox1.Text = "" Or ComboBox2.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            Exit Sub
        Else
            Call carikode()
            If Not dr.HasRows Then
                Dim simpan As String = "insert into tb_paket values ('" & TextBox1.Text & "','" & ComboBox2.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"
                cmd = New OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                Call refeshgrid()
                Call kosongkan()
                Call tampilgrid()
                Call kode()
            End If
        End If
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
            Dim hapus As String = "delete from tb_paket where id='" & TextBox1.Text & "'"
            cmd = New OleDbCommand(hapus, conn)
            cmd.ExecuteNonQuery()
            Call refeshgrid()
            Call kosongkan()
            Call tampilgrid()
            Call kode()
        Else
            Call kosongkan()
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

    Private Sub DataGridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Dim strrownumber As String = (e.RowIndex + 1).ToString
        Dim size As SizeF = e.Graphics.MeasureString(strrownumber, Me.Font)
        If DataGridView1.RowHeadersWidth < CInt((size.Width + 20)) Then
            DataGridView1.RowHeadersWidth = CInt((size.Width + 20))
        End If
        Dim b As Brush = SystemBrushes.ControlText
        e.Graphics.DrawString(strrownumber, Me.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        da = New OleDbDataAdapter("select * from tb_paket where package_name like'%" & TextBox5.Text & "%'or price like '%" & TextBox5.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If TextBox1.Text = "" Or ComboBox2.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("data is incomplate", vbInformation, "Sorry!")
            Exit Sub
        Else
            Call carikode()
            If dr.HasRows Then
                Dim edit As String = "update tb_paket set type='" & ComboBox2.Text & "',package_name='" & TextBox2.Text & "',price='" & TextBox3.Text & "'where id='" & TextBox1.Text & "'"
                cmd = New OleDbCommand(edit, conn)
                cmd.ExecuteNonQuery()
                MsgBox("successful data change!", vbInformation, "Information!")
                Call refeshgrid()
                Call kosongkan()
                Call tampilgrid()
                Call kode()
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        Call kosongkan()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("Are you sure you want to log-out??", "confirm!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            formlogin.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        mainmenuadmin.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        formtransaksiterima.kosongkan()
        formtransaksiterima.kode()
        formtransaksiterima.Show()
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

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        formuserdata.kosongkan()
        formuserdata.kode()
        formuserdata.Show()
        Me.Hide()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        formrekapmasuk.tampilgridtrans()
        formrekapmasuk.refeshgridtrans()
        formrekapmasuk.Show()
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

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        formrekapambil.tampilgridtrans()
        formrekapambil.refeshgridtrans()
        formrekapambil.Show()
        Me.Hide()
    End Sub
End Class