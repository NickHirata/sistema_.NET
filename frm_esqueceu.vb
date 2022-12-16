

Public Class frm_esqueceu


    Private Sub btn_redefinir_Click(sender As Object, e As EventArgs) Handles btn_redefinir.Click
        If txt_email.Text = "" Or txt_cpf.MaskCompleted = False Or txt_csenha.Text = "" Or txt_nsenha.Text = "" Then
            MsgBox("Preencha todos os campos!", MsgBoxStyle.Exclamation + vbOKOnly, "ATENÇÃO")
        Else
            If txt_csenha.Text <> txt_nsenha.Text Then
                MsgBox("Senhas nao conferem!", MsgBoxStyle.Exclamation + vbOKOnly, "ATENÇÃO")

            Else
                SQL = "select * from tb_login where cpf=  '" & txt_cpf.Text & "' "
                rs = db.Execute(SQL)
                If rs.EOF = True Then
                    MsgBox("CPF nao encontrado!", MsgBoxStyle.Exclamation + vbOKOnly, "ATENÇÃO")
                    txt_cpf.Clear()
                Else
                    SQL = "select * from tb_login where email=  '" & txt_email.Text & "' "
                    rs = db.Execute(SQL)
                    If rs.EOF = True Then
                        MsgBox("E-mail nao encontrado!", MsgBoxStyle.Exclamation + vbOKOnly, "ATENÇÃO")
                        txt_email.Clear()
                    Else
                        SQL = "update tb_login set senha='" & txt_csenha.Text & "' where cpf='" & txt_cpf.Text & "' "
                        rs = db.Execute(SQL)
                        MsgBox("Senha atualizada com sucesso!", MsgBoxStyle.Information + vbOKOnly, "AVISO")
                        frm_login.lbl_esqueceu.Visible = False
                        Me.Hide()
                        frm_login.ShowDialog()


                    End If
                End If
            End If
        End If
    End Sub



    Private Sub frm_esqueceu_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then

            SelectNextControl(ActiveControl, e.Shift, True, True, True)
        End If

    End Sub



    Private Sub txt_cpf_LostFocus(sender As Object, e As EventArgs) Handles txt_cpf.LostFocus
        If txt_email.Text = "" Or txt_cpf.MaskCompleted = False Then
            lbl_p.Visible = True
            txt_csenha.Visible = False
            txt_nsenha.Visible = False
            Label4.Visible = False
            Label3.Visible = False
            visu.Visible = False

        Else
            Label4.Visible = True
            Label3.Visible = True
            txt_csenha.Visible = True
            txt_nsenha.Visible = True
            visu.Visible = True
            lbl_p.Visible = False
        End If
    End Sub

    Private Sub frm_esqueceu_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Close()
        frm_login.ShowDialog()



    End Sub

    Private Sub visu_Click(sender As Object, e As EventArgs) Handles visu.Click
        If txt_csenha.PasswordChar = "•" And txt_nsenha.PasswordChar = "•" Then
            txt_csenha.PasswordChar = ""
            txt_nsenha.PasswordChar = ""
        Else
            txt_csenha.PasswordChar = "•"
            txt_nsenha.PasswordChar = "•"
        End If
    End Sub

    Private Sub frm_esqueceu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conecta_banco()
        Label4.Visible = False
        Label3.Visible = False
        txt_csenha.Visible = False
        txt_nsenha.Visible = False
        lbl_p.Visible = False
        visu.Visible = False
        btn_redefinir.BackColor = ColorTranslator.FromHtml("#ff5a00")
        btn_redefinir.ForeColor = ColorTranslator.FromHtml("#fcddcc")
    End Sub
End Class