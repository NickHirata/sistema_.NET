Public Class frm_precificacao

    Private Sub frm_precificacao_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conecta_banco()
        Me.BackColor = ColorTranslator.FromHtml("#2a2a32")
        Button1.BackColor = ColorTranslator.FromHtml("#ff5a00")
        Label6.ForeColor = ColorTranslator.FromHtml("#82828a")
        txt_desconto.ForeColor = ColorTranslator.FromHtml("#82828a")
        txt_nome.ForeColor = ColorTranslator.FromHtml("#82828a")
        txt_lucro.ForeColor = ColorTranslator.FromHtml("#82828a")
        Label3.BackColor = ColorTranslator.FromHtml("#ff5a00")
        Button1.ForeColor = ColorTranslator.FromHtml("#fcddcc")
        Label3.ForeColor = ColorTranslator.FromHtml("#fcddcc")


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If txt_desconto.Text = "" Or txt_lucro.Text = "" Or txt_nome.Text = "" Then
                MsgBox("Preencha todos os campos!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "ATENÇÃO")
            Else
                SQL = "select * from tb_preco where nome= '" & txt_nome.Text & "'"
                rs = db.Execute(SQL)
                If rs.EOF = True Then
                    SQL = "insert into tb_preco (nome,lucro,desconto)  values ('" & txt_nome.Text & "', " &
                        " '" & txt_lucro.Text & "', '" & txt_desconto.Text & "') "
                    rs = db.Execute(SQL)
                    MsgBox("Precificação criada com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    Me.Close()
                    txt_nome.Clear()
                    txt_desconto.Clear()
                    txt_lucro.Clear()
                    With frm_gerente.dgv_dados_estoque
                        carregar_dados_preco()
                    End With
                Else
                    MsgBox("Nome já existente!" + vbNewLine &
                           "Tente outro!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "ATENÇÃO")
                    txt_nome.Clear()
                End If

            End If
        Catch ex As Exception

            MsgBox("Erro ao criar preço!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "AVISO")
        End Try
    End Sub

    Private Sub frm_precificacao_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then

            SelectNextControl(ActiveControl, e.Shift, True, True, True)
        End If
    End Sub
End Class