Imports System.Data.OleDb
Public Class formrekapmasuk
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
    Private Sub formrekapmasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Call koneksi()
        Call tampilgridtrans()
        Call refeshgridtrans()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        mainmenuadmin.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        formcustomer.kosongkan()
        formcustomer.kode()
        formcustomer.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        formdataoutlet.kosongkan()
        formdataoutlet.kode()
        formdataoutlet.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        formuserdata.kosongkan()
        formuserdata.kode()
        formuserdata.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
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

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
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