Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conecta_banco()
        carregar_dados_adm()
        carregar_gerencial()
        With cmb_cargo.Items
            .Add("Administrador")
            .Add("Vendedor")
            .Add("Gerente")
        End With


        lbl_nome.Text = nome
        btn_cadastrar.Visible = True
        btn_editar.Visible = False



        With cmb_pesquisa.Items
            .Add("Código Produto")
            .Add("ID Cliente")
            .Add("Vendedor")

        End With
        cmb_pesquisa.SelectedIndex = 1

        Me.BackColor = ColorTranslator.FromHtml("#2a2a32")
        lbl_nome.ForeColor = ColorTranslator.FromHtml("#fcddcc")
        dgv_dados.BackgroundColor = ColorTranslator.FromHtml("#2a2a32")
        TabPage2.BackColor = ColorTranslator.FromHtml("#161616")
        btn_editar.BackColor = ColorTranslator.FromHtml("#ff5a00")
        btn_cadastrar.BackColor = ColorTranslator.FromHtml("#ff5a00")
        dgv_gerencial.BackgroundColor = ColorTranslator.FromHtml("#2a2a32")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try


            resp = MsgBox("Deseja mesmo encerrar sessão?", MsgBoxStyle.Question + vbYesNo, "AVISO")
            If resp = MsgBoxResult.Yes Then
                SQL = "update tb_acesso set saida= '" & TimeOfDay & "' where nome= '" & nome & "' and entrada= '" & acesso & "'"
                rs = db.Execute(SQL)
                Me.Close()

            End If



        Catch ex As Exception
            MsgBox("Erro ao deslogar!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try

    End Sub

    Private Sub btn_cadastrar_Click(sender As Object, e As EventArgs) Handles btn_cadastrar.Click
        Try
            If txt_email.Text = "" Or txt_fone.Text = "" Or txt_nome.Text = "" Or
                    txt_senha.Text = "" Then
                MsgBox("Preencha todos os campos!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "ATENÇÃO")
            Else
                SQL = "select * from tb_login where cpf = '" & txt_cpf.Text & "' "
                rs = db.Execute(SQL)
                If rs.EOF = True Then
                    SQL = "insert into tb_login values ('" & txt_cpf.Text & "', " &
                        " '" & txt_nome.Text & "','" & txt_email.Text & "', '" & txt_senha.Text & "', " &
                        " '" & cmb_cargo.Text & "' , '" & txt_fone.Text & "' , 'ATIVA' )"
                    rs = db.Execute(SQL)

                    MsgBox("Funcionário cadastrado com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    txt_cpf.Clear()
                    txt_email.Clear()
                    txt_fone.Clear()
                    cmb_cargo.SelectedIndex = -1
                    txt_nome.Clear()
                    txt_senha.Clear()
                    txt_cpf.Focus()
                    carregar_dados_adm()

                Else
                    MsgBox("Funcionário ja cadastrado!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    btn_cadastrar.Visible = False
                    btn_editar.Visible = True

                End If


            End If
        Catch ex As Exception
            MsgBox("Erro ao cadastrar!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")

        End Try
    End Sub




    Private Sub txt_cpf_LostFocus(sender As Object, e As EventArgs) Handles txt_cpf.LostFocus
        SQL = "select * from tb_login where cpf = '" & txt_cpf.Text & "' "
        rs = db.Execute(SQL)
        If rs.EOF = False Then
            btn_cadastrar.Visible = False
            btn_editar.Visible = True

            MsgBox("Funcionário já cadastrado!" + vbNewLine &
                "Deseja editar? ", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "AVISO")
            If vbNo = True Then
                txt_cpf.Clear()
                txt_email.Clear()
                txt_fone.Clear()
                txt_nome.Clear()
                txt_senha.Clear()
                cmb_cargo.SelectedIndex = -1
                txt_cpf.Focus()
                btn_cadastrar.Visible = True
                btn_editar.Visible = False
            Else
                txt_nome.Text = rs.Fields(1).Value
                txt_email.Text = rs.Fields(2).Value
                txt_senha.Text = rs.Fields(3).Value
                cmb_cargo.Text = rs.Fields(4).Value
                txt_fone.Text = rs.Fields(5).Value
                carregar_dados_adm()
            End If


        Else
            txt_email.Clear()
            txt_fone.Clear()
            txt_nome.Clear()
            txt_senha.Clear()
            cmb_cargo.SelectedIndex = -1
            btn_cadastrar.Visible = True
            btn_editar.Visible = False
        End If

    End Sub

    Private Sub btn_editar_Click(sender As Object, e As EventArgs) Handles btn_editar.Click
        Try
            SQL = "update tb_login set nome='" & txt_nome.Text & "', email= '" & txt_email.Text & "', senha= '" & txt_senha.Text & "', " &
                        " cargo='" & cmb_cargo.Text & "' , telefone= '" & txt_fone.Text & "', status='ATIVA' where cpf= '" & txt_cpf.Text & "' "
            rs = db.Execute(SQL)
            MsgBox("Dados atualizados com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
            txt_email.Clear()
            txt_fone.Clear()
            txt_nome.Clear()
            txt_senha.Clear()
            txt_cpf.Clear()
            cmb_cargo.SelectedIndex = -1
            txt_cpf.Focus()

            carregar_dados_adm()
            btn_cadastrar.Visible = True
            btn_editar.Visible = False
        Catch ex As Exception
            MsgBox("ERRO AO ATUALIZAR DADOS!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles visu.Click
        If txt_senha.PasswordChar = "•" Then
            txt_senha.PasswordChar = ""
        Else
            txt_senha.PasswordChar = "•"
        End If
    End Sub

    Private Sub dgv_dados_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_dados.CellContentClick
        Try
            With dgv_dados
                If .CurrentRow.Cells(5).Selected = True Then
                    aux_email = .CurrentRow.Cells(1).Value
                    SQL = "select * from tb_login where email='" & aux_email & "'"
                    rs = db.Execute(SQL)
                    If rs.EOF = False Then
                        TabControl1.SelectTab(1)
                        txt_cpf.Text = rs.Fields(0).Value
                        txt_nome.Text = rs.Fields(1).Value
                        txt_email.Text = rs.Fields(2).Value
                        txt_senha.Text = rs.Fields(3).Value
                        cmb_cargo.Text = rs.Fields(4).Value
                        txt_fone.Text = rs.Fields(5).Value
                        btn_cadastrar.Visible = False
                        btn_editar.Visible = True
                    End If
                ElseIf .CurrentRow.Cells(4).Selected = True Then
                    aux_email = .CurrentRow.Cells(1).Value
                    SQL = "select * from tb_login where email='" & aux_email & "'"
                    rs = db.Execute(SQL)
                    If rs.EOF = False Then
                        status = rs.Fields(6).Value
                        carg = rs.Fields(4).Value
                        If carg = "Administrador" Then
                            MsgBox("Não é possível bloquear a conta do administrador!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                        Else
                            If status = "ATIVA" Then
                                SQL = "update tb_login set status='BLOQUEADO' where email='" & aux_email & "' "
                                rs = db.Execute(SQL)

                                MsgBox("Conta bloqueada com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                                carregar_dados_adm()
                            ElseIf status = "BLOQUEADO" Then
                                SQL = "update tb_login set status='ATIVA'  where email='" & aux_email & "'"
                                rs = db.Execute(SQL)

                                MsgBox("Conta ativada com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                                carregar_dados_adm()
                            End If

                        End If

                    End If

                    End If


            End With
        Catch ex As Exception
            MsgBox("ERRO AO ATUALIZAR DADOS!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then

            SelectNextControl(ActiveControl, e.Shift, True, True, True)
        End If
    End Sub

    Private Sub txt_busc_Click(sender As Object, e As EventArgs) Handles txt_busc.Click

    End Sub

    Private Sub txt_busc_TextChanged(sender As Object, e As EventArgs) Handles txt_busc.TextChanged
        Try

            If cmb_pesquisa.Text = "Código Produto" Then
                aux = "cod_prod"
            ElseIf cmb_pesquisa.Text = "ID Cliente" Then
                aux = "id_cliente"
            Else
                aux = "vendedor"
            End If

            SQL = "select * from tb_vendas where " & aux & " like '" & txt_busc.Text & "%'"
            rs = db.Execute(SQL)

            With dgv_gerencial
                .Rows.Clear()
                Do While rs.EOF = False
                    .Rows.Add(rs.Fields(2).Value, rs.Fields(1).Value, rs.Fields(3).Value, rs.Fields(4).Value, rs.Fields(7).Value)
                    rs.MoveNext()

                Loop
            End With
        Catch ex As Exception
            MsgBox("Erro ao carregar dados", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Private Sub dgv_gerencial_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_gerencial.CellContentClick

    End Sub
End Class

