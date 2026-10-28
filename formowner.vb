Imports System.Data.OleDb
Public Class formowner
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
        da = New OleDbDataAdapter("select * from tb_transaksikeluar ORDER BY id DESC", conn)
        ds = New DataSet
        da.Fill(ds, "tb_transaksikeluar")
        DataGridView1.DataSource = ds.Tables("tb_transaksikeluar")
        '    'responsive dg
        '    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Sub refeshgridtrans()
        da = New OleDbDataAdapter("select * from tb_transaksikeluar", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tb_transaksikeluar")
        DataGridView1.DataSource = (ds.Tables("tb_transaksikeluar"))
    End Sub
    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        da = New OleDbDataAdapter("select * from tb_transaksikeluar where member_name like'%" & TextBox5.Text & "%'or id_outlet like '%" & TextBox5.Text & "%'or user like '%" & TextBox5.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub

    Private Sub formowner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Call tampilgridtrans()
        Call refeshgridtrans()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If MessageBox.Show("Are you sure you want to log-out??", "confirm!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            formlogin.TextBox1.Clear()
            formlogin.TextBox2.Clear()
            formlogin.Show()
            Me.Hide()
        End If
    End Sub
End Class