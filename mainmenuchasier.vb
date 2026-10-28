Imports System.Data.OleDb
Public Class mainmenuchasier
    Private Sub Button9_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("Are you sure you want to log-out??", "confirm!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            formlogin.Show()
            Me.Hide()
            formlogin.TextBox1.Clear()
            formlogin.TextBox2.Clear()
        End If
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

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        customerkasir.kosongkan()
        customerkasir.kode()
        customerkasir.Show()
        Me.Hide()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        transaksiterimakasir.kosongkan()
        transaksiterimakasir.kode()
        transaksiterimakasir.Show()
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        rekapterimakasir.tampilgridtrans()
        rekapterimakasir.refeshgridtrans()
        rekapterimakasir.Show()
        Me.Hide()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        transaksiambilkasir.kosongkan()
        transaksiambilkasir.kodetrans()
        transaksiambilkasir.Show()
        Me.Hide()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        rekapambilkasir.tampilgridtrans()
        rekapambilkasir.refeshgridtrans()
        rekapambilkasir.Show()
        Me.Hide()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        transaksiambilkasir.kosongkan()
        transaksiambilkasir.kodetrans()
        transaksiambilkasir.Show()
        Me.Hide()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        rekapambilkasir.tampilgridtrans()
        rekapambilkasir.refeshgridtrans()
        rekapambilkasir.Show()
        Me.Hide()
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        rekapterimakasir.tampilgridtrans()
        rekapterimakasir.refeshgridtrans()
        rekapterimakasir.Show()
        Me.Hide()

    End Sub
End Class