Imports System.Security.Cryptography.X509Certificates
Imports System.Xml

Public Class frm_vendedor
    Private Sub frm_vendedor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbl_nome.Text = nome
        conecta_banco()
        lbl_cnpj.Visible = True
        lbl_nempresa.Visible = True
        txt_cnpj.Visible = True
        txt_nempresa.Visible = True
        SQL = "select * from tb_preco"
        rs = db.Execute(SQL)
        Do While rs.EOF = False
            With cmb_preco.Items
                .Add(rs.Fields(1).Value)
                rs.MoveNext()
            End With
        Loop
        carregar_dados_estoque()
        gb_venda.Visible = False
        btn_finalizar.Enabled = True
        lbl_obs.Visible = False
        btn_atualizar.Visible = False
        lbl_id.Visible = False
        lbl_cli.Visible = False

        carregar_carrinho()
        carregar_gerencial()

        Me.BackColor = ColorTranslator.FromHtml("#2a2a32")
        TabPage2.BackColor = ColorTranslator.FromHtml("#161616")
        btn_finalizar.BackColor = ColorTranslator.FromHtml("#ff5a00")
        GroupBox1.BackColor = ColorTranslator.FromHtml("#2a2a32")
        gb_venda.BackColor = ColorTranslator.FromHtml("#2a2a32")
        btn_adicionar.BackColor = ColorTranslator.FromHtml("#ff5a00")

        With cmb_pesquisa.Items
            .Add("Código Produto")
            .Add("ID Cliente")
            .Add("Vendedor")

        End With

        SQL = "select * from tb_marca"
        rs = db.Execute(SQL)
        Do While rs.EOF = False
            With cmb_busca.Items
                .Add(rs.Fields(1).Value)
                rs.MoveNext()
            End With
        Loop
        cmb_busca.Items.Add("EM FALTA")
        cmb_busca.Items.Add("TODOS")
        cmb_busca.Items.Add("POR CÓDIGO DO PRODUTO")

        cmb_busca.SelectedIndex = 10



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            resp = MsgBox("Dejesa mesmo encerrar sessão?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "AVISO")
            If resp = MsgBoxResult.Yes Then
                SQL = "update tb_acesso set saida='" & TimeOfDay & "' where nome='" & nome & "' and entrada= '" & acesso & "'"
                rs = db.Execute(SQL)

                SQL = "delete * from tb_vendas where id_cliente= " & aux_cliente & " and status='RASCUNHO' "
                rs = db.Execute(SQL)

                Me.Close()
            End If


        Catch ex As Exception
            MsgBox("Erro ao deslogar!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            If CheckBox1.Checked = False Then
                lbl_cnpj.Visible = True
                lbl_nempresa.Visible = True
                txt_cnpj.Visible = True
                txt_nempresa.Visible = True
                cont = 0
                gb_venda.Visible = False

            Else
                lbl_cnpj.Visible = False
                lbl_nempresa.Visible = False
                txt_cnpj.Visible = False
                txt_nempresa.Visible = False
                cont = cont + 50

            End If

            ProgressBar1.Value = cont
        Catch
            MsgBox("Erro! Contate o técnico de T.I", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub



    Private Sub cmb_preco_LostFocus(sender As Object, e As EventArgs) Handles cmb_preco.LostFocus
        carregar_lbl_preco()
    End Sub



    Private Sub btn_adicionar_Click(sender As Object, e As EventArgs) Handles btn_adicionar.Click

        TabControl1.SelectTab(1)
        carregar_dados_estoque()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_avancar.Click
        Try
            If CheckBox1.Checked = True Then
                If txt_cliente.Text = "" Or txt_cpf.MaskCompleted = False Or txt_fone.MaskCompleted = False Then
                    gb_venda.Visible = False
                    MsgBox("Preencha todos os campos!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    ProgressBar1.Value = 50
                Else
                    SQL = "select * from tb_cliente where cpf='" & txt_cpf.Text & "' "
                    rs = db.Execute(SQL)
                    If rs.EOF = True Then
                        SQL = "insert into tb_cliente (nome_cliente,cpf,telefone,nome_empresa) values ('" & txt_cliente.Text & "', " &
                    " '" & txt_cpf.Text & "', '" & txt_fone.Text & "', 'Não possui empresa' )"
                        rs = db.Execute(SQL)
                        MsgBox("Cliente cadastrado!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    Else
                        MsgBox("Cliente ja existente!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    End If
                    gb_venda.Visible = True
                    ProgressBar1.Value = 100
                    btn_atualizar.Visible = True
                    btn_avancar.Visible = False
                End If
            Else
                If txt_cliente.Text = "" Or txt_cpf.MaskCompleted = False Or txt_fone.MaskCompleted = False Or
                txt_nempresa.Text = "" Or txt_cnpj.MaskCompleted = False Then
                    gb_venda.Visible = False
                    MsgBox("Preencha todos os campos!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    ProgressBar1.Value = 0

                Else
                    SQL = "select * from tb_cliente where cpf='" & txt_cpf.Text & "' "
                    rs = db.Execute(SQL)
                    If rs.EOF = True Then
                        SQL = "insert into tb_cliente (nome_cliente,cpf,telefone,nome_empresa,cnpj) values ('" & txt_cliente.Text & "', " &
                    " '" & txt_cpf.Text & "', '" & txt_fone.Text & "', '" & txt_nempresa.Text & "', '" & txt_cnpj.Text & "' )"
                        rs = db.Execute(SQL)
                        MsgBox("Cliente cadastrado!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    Else
                        MsgBox("Cliente ja existente!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    End If
                    gb_venda.Visible = True
                    ProgressBar1.Value = 100
                    btn_atualizar.Visible = True
                    btn_avancar.Visible = False
                End If
            End If

            aux_cpf = txt_cpf.Text
        Catch
            MsgBox("Erro! Contate o técnico de T.I", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Private Sub Button1_MouseMove(sender As Object, e As MouseEventArgs) Handles btn_avancar.MouseMove
        lbl_obs.Visible = True
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles btn_avancar.MouseLeave
        lbl_obs.Visible = False
    End Sub

    Private Sub btn_atualizar_Click(sender As Object, e As EventArgs) Handles btn_atualizar.Click
        Try
            If CheckBox1.Checked = True Then
                If txt_cliente.Text = "" Or txt_cpf.MaskCompleted = False Or txt_fone.MaskCompleted = False Then
                    gb_venda.Visible = False
                    MsgBox("Preencha todos os campos!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    ProgressBar1.Value = 50
                Else
                    SQL = "select * from tb_cliente where cpf='" & txt_cpf.Text & "' "
                    rs = db.Execute(SQL)
                    If rs.EOF = False Then
                        SQL = "update tb_cliente set nome_cliente='" & txt_cliente.Text & "', telefone= '" & txt_fone.Text & "' " &
                            ", nome_empresa= 'Não possui empresa', cnpj= '' where cpf= '" & txt_cpf.Text & "' "
                        rs = db.Execute(SQL)
                        gb_venda.Visible = True
                        MsgBox("Dados atualizados com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                        ProgressBar1.Value = 100

                        gb_venda.Visible = True
                        ProgressBar1.Value = 100

                    End If
                End If
            Else
                If txt_cliente.Text = "" Or txt_cpf.MaskCompleted = False Or txt_fone.MaskCompleted = False Or
                    txt_nempresa.Text = "" Or txt_cnpj.MaskCompleted = False Then
                    gb_venda.Visible = False
                    MsgBox("Preencha todos os campos!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    ProgressBar1.Value = 0

                Else
                    SQL = "select * from tb_cliente where cpf='" & txt_cpf.Text & "' "
                    rs = db.Execute(SQL)
                    If rs.EOF = False Then
                        SQL = "update tb_cliente set nome_cliente='" & txt_cliente.Text & "', telefone= '" & txt_fone.Text & "' " &
                        ", nome_empresa= '" & txt_nempresa.Text & "', cnpj= '" & txt_cnpj.Text & "' where cpf= '" & txt_cpf.Text & "' "
                        rs = db.Execute(SQL)
                        gb_venda.Visible = True
                        MsgBox("Dados atualizados com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                        ProgressBar1.Value = 100
                        gb_venda.Visible = True
                        ProgressBar1.Value = 100
                    End If
                End If
            End If
        Catch
            MsgBox("Erro! Contate o técnico de T.I", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Private Sub btn_atualizar_MouseLeave(sender As Object, e As EventArgs) Handles btn_atualizar.MouseLeave
        lbl_obs.Visible = False
    End Sub

    Private Sub btn_atualizar_MouseMove(sender As Object, e As MouseEventArgs) Handles btn_atualizar.MouseMove
        lbl_obs.Visible = True
    End Sub


    Private Sub txt_cpf_LostFocus(sender As Object, e As EventArgs) Handles txt_cpf.LostFocus
        SQL = "select * from tb_cliente where cpf='" & txt_cpf.Text & "' "
        rs = db.Execute(SQL)
        If rs.EOF = False Then
            txt_cliente.Text = rs.Fields(1).Value
            txt_cpf.Text = rs.Fields(2).Value
            txt_fone.Text = rs.Fields(3).Value
            aux = rs.Fields(4).Value


            If aux = "Não possui empresa" Then
                CheckBox1.Checked = True
                btn_atualizar.Visible = True
                btn_avancar.Visible = False
                gb_venda.Visible = True
                aux_cpf = txt_cpf.Text
            Else
                txt_nempresa.Text = rs.Fields(4).Value
                txt_cnpj.Text = rs.Fields(5).Value
                btn_atualizar.Visible = True
                btn_avancar.Visible = False
                gb_venda.Visible = True
                aux_cpf = txt_cpf.Text

            End If
        Else
            txt_cliente.Clear()
            CheckBox1.Checked = False
            txt_fone.Clear()
            txt_cnpj.Clear()
            txt_nempresa.Clear()
            btn_avancar.Visible = True
            btn_atualizar.Visible = False
            gb_venda.Visible = False
            carregar_carrinho()

        End If


    End Sub

    Private Sub dgv_estoque_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_estoque.CellContentClick
        Try

            With dgv_estoque
                If .CurrentRow.Cells(5).Selected = True Then
                    aux_cod = .CurrentRow.Cells(0).Value
                    aux_prod = .CurrentRow.Cells(1).Value
                    aux_preco = .CurrentRow.Cells(4).Value
                    aux_preco = Convert.ToDouble(aux_preco).ToString("C")

                    SQL = "select * from tb_cliente where cpf='" & txt_cpf.Text & "' "
                    rs = db.Execute(SQL)
                    If rs.EOF = False Then

                        data_hoje = Now.Date

                        aux_cliente = rs.Fields(0).Value
                        lbl_id.Visible = True
                        lbl_cli.Visible = True
                        lbl_id.Text = aux_cliente
                        SQL = "select * from tb_vendas where id_cliente=" & aux_cliente & " and cod_prod=" & aux_cod & " and status='RASCUNHO'"
                        rs = db.Execute(SQL)
                        If rs.EOF = False Then
                            resp = MsgBox("Produto já adicionado ao carrinho!" + vbNewLine &
                                   "Deseja alterar a quantidade", MsgBoxStyle.Question + vbYesNo, "ATENÇÃO")
                            carregar_carrinho()
                            If resp = MsgBoxResult.Yes Then
                                TabControl1.SelectTab(0)
                                carregar_carrinho()

                            End If
                        Else
                            data_hoje = Now.Date
                            SQL = "insert into tb_vendas (id_cliente,cod_prod,qtde,preco_unit,status,data,vendedor,prod) values ('" & aux_cliente & "', " &
                               " '" & aux_cod & "', '1', '" & aux_preco & "', 'RASCUNHO', '" & data_hoje & "','" & lbl_nome.Text & "','" & aux_prod & "') "
                            rs = db.Execute(SQL)

                            NotifyIcon1.ShowBalloonTip(5000, "PRODUTO ADICIONADO COM SUCESSO!", "Produto " & aux_cod & " foi adicionado ao carrinho do cliente " & aux_cpf & "", ToolTipIcon.Info)
                            carregar_carrinho()
                        End If

                    Else
                        MsgBox("Cliente não encontrado!" + vbNewLine &
                                    "Atualize ou preencha os campos do cliente", MsgBoxStyle.Exclamation + vbOKOnly, "ATENÇÃO")
                        TabControl1.SelectTab(0)
                    End If
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub
    Private Sub dgv_geren_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_geren.CellContentClick
        Try
            Dim id As Integer
            With dgv_geren
                If .CurrentRow.Cells(1).Selected = True Then
                    id = .CurrentRow.Cells(1).Value
                    SQL = "select * from tb_cliente where id_cliente= " & id & ""
                    rs = db.Execute(SQL)
                    If rs.EOF = False Then
                        MsgBox("ID: " & id & " " + vbNewLine &
                               "CPF: " & rs.Fields(2).Value & " " + vbNewLine &
                               "NOME: " & rs.Fields(1).Value & " ", vbInformation + vbOKOnly, "DADOS DO CLIENTE!")
                    End If
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub
    Private Sub dgv_carrinho_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_carrinho.CellContentClick
        Try
            With dgv_carrinho
                data_hoje = Now.Date


                If .CurrentRow.Cells(4).Selected = True Then
                    aux_cod = .CurrentRow.Cells(0).Value
                    SQL = "select * from tb_vendas  where id_cliente= " & aux_cliente & " and status='RASCUNHO' and cod_prod= " & aux_cod & ""
                    rs = db.Execute(SQL)
                    If rs.EOF = False Then
                        qtd_venda = .CurrentRow.Cells(3).Value + 1
                        SQL = "update tb_vendas set qtde='" & qtd_venda & "' where id_cliente= " & aux_cliente & " and status='RASCUNHO' and cod_prod= " & aux_cod & ""
                        rs = db.Execute(SQL)
                        carregar_lbl_preco()
                    End If

                End If
                If .CurrentRow.Cells(2).Selected = True Then
                    aux_cod = .CurrentRow.Cells(0).Value
                    SQL = "select * from tb_vendas  where id_cliente= " & aux_cliente & " and status='RASCUNHO' and cod_prod= " & aux_cod & ""
                    rs = db.Execute(SQL)
                    If rs.EOF = False Then
                        qtd_venda = .CurrentRow.Cells(3).Value - 1
                        If qtd_venda = 0 Then
                            resp = MsgBox("Deseja mesmo excluir esse produto?", MsgBoxStyle.Question + vbYesNo, "ATENÇÃO")
                            If resp = MsgBoxResult.Yes Then
                                SQL = " delete * from tb_vendas where id_cliente=" & aux_cliente & " and cod_prod= " & aux_cod & " and status='RASCUNHO'"
                                rs = db.Execute(SQL)

                                NotifyIcon1.ShowBalloonTip(5000, "PRODUTO EXCLUÍDO COM SUCESSO!", "Produto " & aux_cod & " foi excluído do carrinho do cliente " & aux_cpf & "", ToolTipIcon.Info)

                                carregar_carrinho()

                            End If

                        Else
                            SQL = "update tb_vendas set qtde=" & qtd_venda & " where id_cliente= " & aux_cliente & " and status='RASCUNHO' and cod_prod=" & aux_cod & " "
                            rs = db.Execute(SQL)
                            carregar_carrinho()

                        End If

                    End If
                End If
            End With

        Catch ex As Exception
            MsgBox("Selecione algum produto!!", MsgBoxStyle.Exclamation + vbOK, "ATENÇÃO")
        End Try

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        MsgBox("Os rascunhos de vendas não serão salvos após fazer logout" + vbNewLine &
            "Finalize as vendas!!", MsgBoxStyle.Exclamation + vbOKOnly, "ATENÇÃO")
    End Sub



    Private Sub btn_cancelar_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Try
            MsgBox("Deseja mesmo cancelar essa venda?" + vbNewLine &
            "Os dados dessa venda serão perdidos!", MsgBoxStyle.Exclamation + vbYesNo, "ATENÇÃO")
            If MsgBoxResult.Yes Then
                SQL = "delete * from tb_vendas where id_cliente= " & aux_cliente & " and status='RASCUNHO'"
                rs = db.Execute(SQL)
                txt_cliente.Clear()
                txt_cpf.Clear()
                txt_cpf.Focus()
                CheckBox1.Checked = False
                txt_fone.Clear()
                txt_cnpj.Clear()
                txt_nempresa.Clear()
                btn_avancar.Visible = True
                btn_atualizar.Visible = False
                gb_venda.Visible = False
                dgv_carrinho.Rows.Clear()
                cmb_preco.SelectedIndex = -1
                carregar_lbl_preco()


            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_finalizar_Click(sender As Object, e As EventArgs) Handles btn_finalizar.Click

        Try


            tirar_estoque()
            data_hoje = Now.Date
            SQL = "update tb_vendas set status= 'FINALIZADO' where id_cliente= " & aux_cliente & " and status='RASCUNHO'"
            rs = db.Execute(SQL)
            SQL = "insert into tb_venda_total (id_cliente,precificacao,valor_pago,data,vendedor) values ('" & aux_cliente & "', " &
            " '" & cmb_preco.Text & "', '" & lbl_vf.Text & "', '" & data_hoje & "','" & lbl_nome.Text & "')"
            rs = db.Execute(SQL)


            SQL = "select * from tb_venda_total  where id_cliente= " & aux_cliente & " and vendedor='" & nome & "' "
            rs = db.Execute(SQL)
            If rs.EOF = False Then
                aux_dvenda = rs.Fields(0).Value
            End If

            SQL = "update tb_vendas set id_venda= " & aux_dvenda & " where id_cliente= " & aux_cliente & " and vendedor= '" & nome & "'"
            rs = db.Execute(SQL)



            MsgBox("Venda finalizada com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")



            txt_cliente.Clear()
            txt_cpf.Clear()
            txt_cpf.Focus()
            CheckBox1.Checked = False
            txt_fone.Clear()
            txt_cnpj.Clear()
            txt_nempresa.Clear()
            cmb_preco.SelectedIndex = -1
            btn_avancar.Visible = True
            btn_atualizar.Visible = False
            gb_venda.Visible = False
            dgv_carrinho.Rows.Clear()
            carregar_lbl_preco()
            carregar_gerencial()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub frm_vendedor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then

            SelectNextControl(ActiveControl, e.Shift, True, True, True)
        End If
    End Sub

    Private Sub btn_busca_Click(sender As Object, e As EventArgs) Handles btn_busca.Click
        Try
            If cmb_busca.Text = "EM FALTA" Then

                If txt_busca.Text = "" Then
                    SQL = "select * from tb_estoque where disponivel < 10"
                    rs = db.Execute(SQL)

                    With dgv_estoque
                        .Rows.Clear()
                        Do While rs.EOF = False
                            .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                            rs.MoveNext()

                        Loop
                    End With

                Else
                    SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%' and disponivel < 10"
                    rs = db.Execute(SQL)

                    With dgv_estoque
                        .Rows.Clear()
                        Do While rs.EOF = False
                            .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                            rs.MoveNext()

                        Loop
                    End With
                End If

            ElseIf cmb_busca.Text = "POR CÓDIGO DO PRODUTO" Then


                SQL = "select * from tb_estoque where ID like '" & txt_busca.Text & "%'"
                rs = db.Execute(SQL)
                With dgv_estoque
                    .Rows.Clear()
                    Do While rs.EOF = False
                        .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                        rs.MoveNext()

                    Loop
                End With

            ElseIf cmb_busca.Text = "TODOS" Then
                SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%'"
                rs = db.Execute(SQL)

                With dgv_estoque
                    .Rows.Clear()
                    Do While rs.EOF = False
                        .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                        rs.MoveNext()

                    Loop
                End With
            Else
                If txt_busca.Text = "" Then
                    SQL = "select * from tb_estoque where marca='" & cmb_busca.Text & "'"
                    rs = db.Execute(SQL)

                    With dgv_estoque
                        .Rows.Clear()
                        Do While rs.EOF = False
                            .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                            rs.MoveNext()

                        Loop
                    End With
                Else
                    SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%' and marca='" & cmb_busca.Text & "'"
                    rs = db.Execute(SQL)

                    With dgv_estoque
                        .Rows.Clear()
                        Do While rs.EOF = False
                            .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                            rs.MoveNext()

                        Loop
                    End With
                End If
            End If
        Catch
            MsgBox("Erro! Contate o técnico de T.I", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub



    Private Sub txt_busca_TextChanged(sender As Object, e As EventArgs) Handles txt_busca.TextChanged
        Try
            If cmb_busca.Text = "EM FALTA" Then

                If txt_busca.Text = "" Then
                    SQL = "select * from tb_estoque where disponivel < 10"
                    rs = db.Execute(SQL)

                    With dgv_estoque
                        .Rows.Clear()
                        Do While rs.EOF = False
                            .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                            rs.MoveNext()

                        Loop
                    End With
                Else
                    SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%' and disponivel < 10"
                    rs = db.Execute(SQL)

                    With dgv_estoque
                        .Rows.Clear()
                        Do While rs.EOF = False
                            .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                            rs.MoveNext()

                        Loop
                    End With
                End If
            ElseIf cmb_busca.Text = "POR CÓDIGO DO PRODUTO" Then


                SQL = "select * from tb_estoque where ID like '" & txt_busca.Text & "%'"
                rs = db.Execute(SQL)

                With dgv_estoque
                    .Rows.Clear()
                    Do While rs.EOF = False
                        .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                        rs.MoveNext()

                    Loop
                End With

            ElseIf cmb_busca.Text = "TODOS" Then
                SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%'"
                rs = db.Execute(SQL)

                With dgv_estoque
                    .Rows.Clear()
                    Do While rs.EOF = False
                        .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                        rs.MoveNext()

                    Loop
                End With
            Else

                SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%' and marca='" & cmb_busca.Text & "'"
                rs = db.Execute(SQL)

                With dgv_estoque
                    .Rows.Clear()
                    Do While rs.EOF = False
                        .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                        rs.MoveNext()

                    Loop
                End With
            End If

        Catch ex As Exception
            MsgBox("Erro ao carregar dados", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        carregar_lbl_preco()
        NotifyIcon1.ShowBalloonTip(5000, "PREÇO ATUALIZADO COM SUCESSO!", " ", ToolTipIcon.Info)

    End Sub

    Private Sub PictureBox2_MouseHover(sender As Object, e As EventArgs) Handles PictureBox2.MouseHover
        ToolTip1.Show("Clique para atualizar os valores, se necessário!!", PictureBox2)
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

            With dgv_geren
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


End Class