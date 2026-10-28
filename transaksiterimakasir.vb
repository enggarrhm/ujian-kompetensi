Imports System.Data.OleDb
Public Class transaksiterimakasir
    Dim bilangan As Integer = 1
    Sub kosongkan()
        cbmember.Text = ""
        txtmember.Clear()
        cbitem.Text = ""
        txtpackage.Clear()
        txttype.Clear()
        txtprice.Clear()
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        txtqty.Clear()
        txttitem.Clear()
        txttprice.Clear()
        txtdisc.Clear()
        txttax.Clear()
        txtttpayment.Clear()
        txtidtrans.Clear()
    End Sub
    Sub kosongkan1()
        cbitem.Text = ""
        txtpackage.Clear()
        txttype.Clear()
        txtprice.Clear()
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        txtqty.Clear()
    End Sub
    Sub databaru()
        cbmember.Text = ""
        txtmember.Clear()
        cbitem.Text = ""
        txtpackage.Clear()
        txttype.Clear()
        txtprice.Clear()
        CheckBox1.Text = ""
        CheckBox2.Text = ""
        CheckBox3.Text = ""
        txtcharges.Clear()
        txtqty.Clear()
        txtsubtotal.Clear()
    End Sub
    Sub carikode()
        cmd = New OleDbCommand("select * from tb_keranjang where id ='" & txtidchart.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub
    Sub carikodetrans()
        cmd = New OleDbCommand("select * from tb_transaksi where id ='" & txtidtrans.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub
    Sub tampilgrid()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_keranjang ORDER BY id DESC", conn)
        ds = New DataSet
        da.Fill(ds, "tb_keranjang")
        DataGridView1.DataSource = ds.Tables("tb_keranjang")
        ''responsive dg
        'DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Sub refeshgrid()
        da = New OleDbDataAdapter("select * from tb_keranjang", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tb_keranjang")
        DataGridView1.DataSource = (ds.Tables("tb_keranjang"))
    End Sub
    Sub kodetrans()
        Call koneksi()
        cmd = New OleDbCommand("select * from tb_transaksi where id in(select max(id) from tb_transaksi)", conn)
        Dim urutkode As String
        Dim hitung As Long
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            urutkode = "TR" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(dr.GetString(0), 3) + 1
            urutkode = "TR" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        txtidtrans.Text = urutkode
    End Sub
    Sub kode()
        Call koneksi()
        cmd = New OleDbCommand("select * from tb_keranjang where id in(select max(id) from tb_keranjang)", conn)
        Dim urutkode As String
        Dim hitung As Long
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            urutkode = "KJ" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(dr.GetString(0), 3) + 1
            urutkode = "KJ" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        txtidchart.Text = urutkode
        txtinvoice.Focus()
    End Sub
    Sub simpandatamember()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_member ", conn)
        ds = New DataSet
        da.Fill(ds)
        re.DataSource = ds
        re.DataMember = ds.Tables(0).ToString
    End Sub
    Sub itemcombomember()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_member ", conn)
        ds = New DataSet
        da.Fill(ds)
        re.DataSource = ds
        re.DataMember = ds.Tables(0).ToString
        Dim a As DataRow
        cbmember.Items.Clear()
        For Each a In ds.Tables(0).Rows
            cbmember.Items.Add(a.Item(0))
        Next a
    End Sub
    Sub simpandataitem()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_paket ", conn)
        ds = New DataSet
        da.Fill(ds)
        re.DataSource = ds
        re.DataMember = ds.Tables(0).ToString
    End Sub
    Sub itemcomboitem()
        Call koneksi()
        da = New OleDbDataAdapter("select * from tb_paket ", conn)
        ds = New DataSet
        da.Fill(ds)
        re.DataSource = ds
        re.DataMember = ds.Tables(0).ToString
        Dim a As DataRow
        cbitem.Items.Clear()
        For Each a In ds.Tables(0).Rows
            cbitem.Items.Add(a.Item(0))
        Next a
    End Sub
    Sub kodeinvoice()
        Call koneksi()
        Dim nilai As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890"
        Dim hasil As String = ""
        Dim count = 0
        Dim rand As New Random
        Dim strpos = ""
        While count < 5
            strpos = rand.Next(0, nilai.Length)
            hasil = hasil & nilai(strpos)
            count = count + 1
        End While
        txtinvoice.Text = hasil
    End Sub
    Sub check()
        ' Inisialisasi total
        Dim total As Integer = 0

        ' Jumlahkan nilai CheckBox yang dicentang
        If CheckBox1.Checked Then
            total += 5000
        End If

        If CheckBox2.Checked Then
            total += 2000
        End If

        If CheckBox3.Checked Then
            total += 3000
        End If

        ' Setel teks TextBox dengan nilai total
        txtcharges.Text = total.ToString()
    End Sub
    Sub subtotal()
        txtsubtotal.Text = Val(txtprice.Text) * Val(txtqty.Text) + Val(txtcharges.Text)
    End Sub
    Sub Hasil()
        Dim totalHarga As Double = CDbl(txttprice.Text)
        Dim diskon As Double = totalHarga * 0.12
        Dim totalSetelahDiskon As Double = totalHarga - diskon
        Dim pajak As Double = totalSetelahDiskon * 0.1
        txtdisc.Text = If(diskon Mod 1 = 0, diskon.ToString("F0"), diskon.ToString("F2"))
        txttax.Text = If(pajak Mod 1 = 0, pajak.ToString("F0"), pajak.ToString("F2"))
        txtttpayment.Text = totalHarga - diskon + pajak
    End Sub
    Sub CalTotal()
        Dim hitung As Long = 0
        For baris As Long = 0 To DataGridView1.RowCount - 1
            hitung = hitung + DataGridView1.Rows(baris).Cells(10).Value
        Next
        txttprice.Text = hitung
    End Sub
    Private Sub CalQty()
        Dim hitung As Long = 0
        For baris As Long = 0 To DataGridView1.RowCount - 1
            hitung = hitung + DataGridView1.Rows(baris).Cells(9).Value
        Next
        txttitem.Text = hitung
    End Sub
    Sub KetemuUser()
        Call koneksi()
        da = New OleDbDataAdapter("SELECT * FROM tb_user WHERE password LIKE '%" & formlogin.TextBox2.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds, "tb_user")
        TextBox1.Text = formlogin.TextBox2.Text
    End Sub

    Sub ValidateUser()
        Dim password As String = TextBox1.Text
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=laundry.accdb"
        Dim connection As New OleDbConnection(connectionString)

        Try
            connection.Open()
            Dim query As String = "SELECT name, id_outlet, role FROM tb_user WHERE password = ?"
            Dim cmd As New OleDbCommand(query, connection)
            cmd.Parameters.AddWithValue("@password", password)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                txtuser.Text = reader("name").ToString()
                txtidot.Text = reader("id_outlet").ToString()
                TextBox6.Text = reader("role").ToString()
            Else
                MessageBox.Show("User not found.")
            End If

            reader.Close()
        Catch ex As OleDbException
            MessageBox.Show("OleDb Error: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Sub
    Sub TwoDay()
        Dim today As DateTime = DateTime.Today
        Dim futureDate As DateTime = today.AddDays(2)
        DateTimePicker1.Text = today.ToShortDateString()
        DateTimePicker2.Text = futureDate.ToShortDateString()
    End Sub
    Sub HapusSemuaDataDariDatabase()
        ' Koneksi ke database
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=laundry.accdb"

        ' Query untuk menghapus semua data dari tabel
        Dim query As String = "DELETE FROM tb_keranjang" ' Ganti dengan nama tabel yang sesuai

        Using connection As New OleDbConnection(connectionString)
            Try
                connection.Open()

                ' Eksekusi query
                Using cmd As New OleDbCommand(query, connection)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As OleDbException
                MessageBox.Show("OleDb Error: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try
        End Using
    End Sub

    Sub filtercart()
        da = New OleDbDataAdapter("select * from tb_keranjang where kode_invoice like '%" & txtinvoice.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub
    Private Sub transaksiterimakasir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Call databaru()
        Call koneksi()
        Call tampilgrid()
        Call refeshgrid()
        Call kosongkan()
        Call kode()
        Call kodetrans()
        Call simpandatamember()
        Call itemcombomember()
        Call simpandataitem()
        Call itemcomboitem()
        Call kodeinvoice()
        Call subtotal()
        Call CalTotal()
        Call CalQty()
        Call Hasil()
        Call TwoDay()
        Call KetemuUser()
        Call ValidateUser()
        Call filtercart()
        Call HapusSemuaDataDariDatabase()
        txtidtrans.Visible = False
        txtidot.Visible = False
        txtuser.Visible = False
        DateTimePicker1.Visible = False
        DateTimePicker2.Visible = False
        txttprice.Visible = False
        txtdisc.Visible = False
        txttax.Visible = False
        status.Visible = False
        txtstatuspayment.Visible = False
        TextBox1.Visible = False
        txtcharges.Text = "0"
        txtsubtotal.Text = "0"
        txttitem.Text = "0"
        txttprice.Text = "0"
        txtdisc.Text = "0"
        txttax.Text = "0"
        txtttpayment.Text = "0"
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

    Private Sub cbmember_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbmember.SelectedIndexChanged
        Call koneksi()
        cmd = New OleDb.OleDbCommand("select*from tb_member where id='" & cbmember.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        txtmember.Text = dr.Item(1)
    End Sub

    Private Sub cbitem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbitem.SelectedIndexChanged
        Call koneksi()
        cmd = New OleDb.OleDbCommand("select*from tb_paket where id='" & cbitem.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        txtpackage.Text = dr.Item(2)
        txttype.Text = dr.Item(1)
        txtprice.Text = dr.Item(3)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Call check()
        Call subtotal()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Call check()
        Call subtotal()
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        Call check()
        Call subtotal()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If bilangan > 1 Then
            bilangan -= 1
            txtqty.Text = bilangan.ToString()
            Call subtotal()
        Else
            MessageBox.Show("The number can't be less than 1", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        bilangan += 1
        txtqty.Text = bilangan.ToString()
        Call subtotal()
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        If txtidchart.Text = "" Or txtinvoice.Text = "" Or cbmember.Text = "" Or txtmember.Text = "" Or cbitem.Text = "" Or cbitem.Text = "" Or txtpackage.Text = "" Or txttype.Text = "" Or txtprice.Text = "" Or txtqty.Text = "" Or txtcharges.Text = "" Or txtsubtotal.Text = "" Then
            Exit Sub
        Else
            Call carikode()
            If Not dr.HasRows Then
                Dim simpan As String = "insert into tb_keranjang values ('" & txtidchart.Text & "','" & txtinvoice.Text & "','" & cbmember.Text & "','" & txtmember.Text & "','" & cbitem.Text & "','" & txtpackage.Text & "','" & txttype.Text & "','" & txtprice.Text & "','" & txtcharges.Text & "','" & txtqty.Text & "','" & txtsubtotal.Text & "')"
                cmd = New OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                Call refeshgrid()
                Call kosongkan1()
                Call tampilgrid()
                Call subtotal()
                Call CalQty()
                Call CalTotal()
                Call Hasil()
                Call kode()
            End If
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        On Error Resume Next
        cmd = New OleDbCommand("select * from tb_keranjang", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            MsgBox("there is no data to delete", vbIgnore, "sorry!")
            Exit Sub
        Else
            cmd = New OleDbCommand("delete from tb_keranjang where id='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value & "'", conn)
            cmd.ExecuteNonQuery()
            MsgBox("data has been successfully deleted!", vbInformation, "confirm!")
            Call tampilgrid()
            Call CalTotal()
            Call CalQty()
            Call subtotal()
            Call Hasil()
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If txtidtrans.Text = "" Or txtinvoice.Text = "" Or txtidot.Text = "" Or txtmember.Text = "" Or txtuser.Text = "" Or DateTimePicker1.Text = "" Or DateTimePicker2.Text = "" Or txttitem.Text = "" Or txttprice.Text = "" Or txtdisc.Text = "" Or txttax.Text = "" Or status.Text = "" Or txtstatuspayment.Text = "" Or txtttpayment.Text = "" Then
            Exit Sub
        Else
            Call carikode()
            If Not dr.HasRows Then
                Dim simpan As String = "insert into tb_transaksi values ('" & txtidtrans.Text & "','" & txtinvoice.Text & "','" & txtidot.Text & "','" & txtmember.Text & "','" & txtuser.Text & "','" & DateTimePicker1.Text & "','" & DateTimePicker2.Text & "','" & txttitem.Text & "','" & txttprice.Text & "','" & txtdisc.Text & "','" & txttax.Text & "','" & status.Text & "','" & txtstatuspayment.Text & "','" & txtttpayment.Text & "')"
                cmd = New OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                If MessageBox.Show("no data changes?", "confirm!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    MsgBox("transaction complete!", vbInformation, "Informasi!")
                    Call kosongkan()
                    Call kodeinvoice()
                    Call kode()
                    Call kodetrans()
                    Call filtercart()
                    Call HapusSemuaDataDariDatabase()
                End If
            End If
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If MessageBox.Show("Are you sure you want to log-out??", "confirm!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            formlogin.TextBox1.Clear()
            formlogin.TextBox2.Clear()
            formlogin.Show()
            Me.Hide()
        End If
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

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        transaksiambilkasir.kosongkan()
        transaksiambilkasir.kodetrans()
        transaksiambilkasir.Show()
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
End Class