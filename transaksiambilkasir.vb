Imports System.Data.OleDb
Public Class transaksiambilkasir
    Sub kosongkan()
        cbidtrans.Text = ""
        txtinvoice.Clear()
        txtidot.Clear()
        txtmember.Clear()
        txtuser.Clear()
        txtdate.Clear()
        txtdeadline.Clear()
        txtqty.Clear()
        txttotal.Clear()
        txtdisc.Clear()
        txttax.Clear()
        txtstatus.Clear()
        txtstatuspayment.Clear()
        txttotalpayment.Clear()
    End Sub
    Sub databaru()
        cbidtrans.Text = ""
        txtinvoice.Clear()
        txtidot.Clear()
        txtmember.Clear()
        txtuser.Clear()
        txtdate.Clear()
        txtdeadline.Clear()
        txtqty.Clear()
        txttotal.Clear()
        txtdisc.Clear()
        txttax.Clear()
        txtstatus.Clear()
        txtstatuspayment.Clear()
        txttotalpayment.Clear()
    End Sub
    Sub ketemu()
        On Error Resume Next
        txtinvoice.Text = dr.Item(1)
        txtidot.Text = dr.Item(2)
        txtmember.Text = dr.Item(3)
        txtuser.Text = dr.Item(4)
        txtdate.Text = dr.Item(5)
        txtdeadline.Text = dr.Item(6)
        txtqty.Text = dr.Item(7)
        txttotal.Text = dr.Item(8)
        txtdisc.Text = dr.Item(9)
        txttax.Text = dr.Item(10)
        txtstatus.Text = dr.Item(11)
        txtstatuspayment.Text = dr.Item(12)
        txttotalpayment.Text = dr.Item(13)
    End Sub
    Sub simpandatatrans()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_transaksi ", conn)
        ds = New DataSet
        da.Fill(ds)
        re.DataSource = ds
        re.DataMember = ds.Tables(0).ToString
    End Sub
    Sub itemcombotrans()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_transaksi ", conn)
        ds = New DataSet
        da.Fill(ds)
        re.DataSource = ds
        re.DataMember = ds.Tables(0).ToString
        Dim a As DataRow
        cbidtrans.Items.Clear()
        For Each a In ds.Tables(0).Rows
            cbidtrans.Items.Add(a.Item(0))
        Next a
    End Sub
    Sub insert()
        If txtid.Text = "" Or txtinvoice.Text = "" Or txtidot.Text = "" Or txtmember.Text = "" Or txtuser.Text = "" Or txtdate.Text = "" Or txtdeadline.Text = "" Or txtqty.Text = "" Or txttotal.Text = "" Or txtdisc.Text = "" Or txttax.Text = "" Or txtstatus.Text = "" Or txtstatuspayment.Text = "" Or txttotalpayment.Text = "" Or DateTimePicker1.Text = "" Then
            Exit Sub
        Else
            Call carikodetrans()
            If Not dr.HasRows Then
                Dim simpan As String = "insert into tb_transaksikeluar values ('" & txtid.Text & "','" & txtinvoice.Text & "','" & txtidot.Text & "','" & txtmember.Text & "','" & txtuser.Text & "','" & txtdate.Text & "','" & txtdeadline.Text & "','" & txtqty.Text & "','" & txttotal.Text & "','" & txtdisc.Text & "','" & txttax.Text & "','" & txtstatusnew.Text & "','" & txtstatuspaymentnew.Text & "','" & txttotalpayment.Text & "','" & DateTimePicker1.Text & "')"
                cmd = New OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                If MessageBox.Show("no data changes?", "confirm!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    MsgBox("transaction complete!", vbInformation, "Informasi!")
                End If
            End If
        End If
    End Sub
    Sub delete()
        If cbidtrans.Text = "" Then
            MsgBox("code must be filled in", vbInformation, "sorry!")
            cbidtrans.Text = Focus()
            Exit Sub
        End If
        Call carikode()
        If Not dr.HasRows Then
            MsgBox("code not found", vbInformation, "sorry!")
            cbidtrans.Focus()
            Exit Sub
        End If
        Dim hapus As String = "delete from tb_transaksi where id='" & cbidtrans.Text & "'"
        cmd = New OleDbCommand(hapus, conn)
        cmd.ExecuteNonQuery()
        Call refeshgridtrans()
        Call tampilgridtrans()
        formrekapambil.refeshgridtrans()
        formrekapambil.tampilgridtrans()
        Call kosongkan()
        Call kodetrans()
    End Sub
    Sub carikodetrans()
        cmd = New OleDbCommand("select * from tb_transaksikeluar where id ='" & txtid.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub
    Sub carikode()
        cmd = New OleDbCommand("select * from tb_transaksi where id ='" & cbidtrans.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
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
    Sub tampilgridtrans()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_transaksi ORDER BY id DESC", conn)
        ds = New DataSet
        da.Fill(ds, "tb_transaksi")
        DataGridView1.DataSource = ds.Tables("tb_transaksi")
        '    'responsive dg
        '    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Sub refeshgridtrans()
        da = New OleDbDataAdapter("select * from tb_transaksi", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tb_transaksi")
        DataGridView1.DataSource = (ds.Tables("tb_transaksi"))
    End Sub
    Sub kodetrans()
        Call koneksi()
        cmd = New OleDbCommand("select * from tb_transaksikeluar where id in(select max(id) from tb_transaksikeluar)", conn)
        Dim urutkode As String
        Dim hitung As Long
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            urutkode = "TRK" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(dr.GetString(0), 3) + 1
            urutkode = "TRK" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        txtid.Text = urutkode
    End Sub
    Private Sub transaksiambilkasir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Call kodetrans()
        Call kodetrans()
        Call kosongkan()
        Call databaru()
        Call tampilgridtrans()
        Call refeshgridtrans()
        Call simpandatatrans()
        Call itemcombotrans()
        txtstatusnew.Text = "Taken"
        txtstatuspaymentnew.Text = "Paid"
        txtinvoice.Visible = False
        txtidot.Visible = False
        txtuser.Visible = False
        txtdate.Visible = False
        txtdeadline.Visible = False
        txttotal.Visible = False
        txtdisc.Visible = False
        txttax.Visible = False
        txtstatus.Visible = False
        txtstatuspayment.Visible = False
        txtstatusnew.Visible = False
        txtstatuspaymentnew.Visible = False
        txtid.Visible = False
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Call insert()
        Call delete()
    End Sub

    Private Sub cbidtrans_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbidtrans.SelectedIndexChanged
        Call koneksi()
        cmd = New OleDb.OleDbCommand("select*from tb_transaksi where id='" & cbidtrans.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        txtinvoice.Text = dr.Item(1)
        txtidot.Text = dr.Item(2)
        txtmember.Text = dr.Item(3)
        txtuser.Text = dr.Item(4)
        txtdate.Text = dr.Item(5)
        txtdeadline.Text = dr.Item(6)
        txtqty.Text = dr.Item(7)
        txttotal.Text = dr.Item(8)
        txtdisc.Text = dr.Item(9)
        txttax.Text = dr.Item(10)
        txtstatus.Text = dr.Item(11)
        txtstatuspayment.Text = dr.Item(12)
        txttotalpayment.Text = dr.Item(13)
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        da = New OleDbDataAdapter("select * from tb_transaksi where member_name like'%" & TextBox5.Text & "%'or id like '%" & TextBox5.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        mainmenuchasier.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        customerkasir.kosongkan()
        customerkasir.kode()
        customerkasir.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        transaksiterimakasir.kosongkan()
        transaksiterimakasir.kode()
        transaksiterimakasir.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        rekapterimakasir.tampilgridtrans()
        rekapterimakasir.refeshgridtrans()
        rekapterimakasir.Show()
        Me.Hide()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        rekapambilkasir.tampilgridtrans()
        rekapambilkasir.refeshgridtrans()
        rekapambilkasir.Show()
        Me.Hide()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If MessageBox.Show("Are you sure you want to log-out??", "confirm!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            formlogin.TextBox1.Clear()
            formlogin.TextBox2.Clear()
            formlogin.Show()
            Me.Hide()
        End If
    End Sub
End Class