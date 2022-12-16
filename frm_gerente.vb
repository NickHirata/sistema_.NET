Imports System.Diagnostics.Eventing.Reader

Public Class frm_gerente
    Private Sub frm_gerente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conecta_banco()
        carregar_dados()
        lbl_nome.Text = nome
        carregar_dados_estoque()
        carregar_dados_preco()
        carregar_marcas()
        carregar_gerencial()
        carregar_financeiro()


        Me.BackColor = ColorTranslator.FromHtml("#2a2a32")
        TabPage1.BackColor = ColorTranslator.FromHtml("#161616")
        btn_cadastrar.BackColor = ColorTranslator.FromHtml("#ff5a00")
        Label8.ForeColor = ColorTranslator.FromHtml("#ff5a00")
        Button1.BackColor = ColorTranslator.FromHtml("#ff5a00")
        TabPage6.BackColor = ColorTranslator.FromHtml("#2a2a32")
        dgv_preco.BackgroundColor = ColorTranslator.FromHtml("#2a2a32")
        dgv_dados.BackgroundColor = ColorTranslator.FromHtml("#2a2a32")
        dgv_dados_estoque.BackgroundColor = ColorTranslator.FromHtml("#2a2a32")

        With cmb_pesquisa.Items
            .Add("Código Produto")
            .Add("ID Cliente")
            .Add("Vendedor")

        End With
        With cmb_tipo.Items
            .Add("NOME")
            .Add("EMAIL")

        End With
        cmb_tipo.SelectedIndex = 0

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

        cmb_pesquisa.SelectedIndex = 0

        cmb_busca.SelectedIndex = 10


        If txt_preco.Text <> "" And txt_qtde.Text <> "" Then
            lbl_total.Text = FormatCurrency(txt_qtde.Text * txt_preco.Text)
        Else
            lbl_total.Text = FormatCurrency(0.00)
        End If
        carregar_dados()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            resp = MsgBox("Dejesa mesmo encerrar sessão?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "AVISO")
            If resp = MsgBoxResult.Yes Then
                SQL = "update tb_acesso set saida='" & TimeOfDay & "' where nome='" & nome & "' and entrada= '" & acesso & "'"
                rs = db.Execute(SQL)

                Me.Close()
            End If


        Catch ex As Exception
            MsgBox("Erro ao deslogar!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        carregar_dados_preco()
        frm_precificacao.ShowDialog()

    End Sub

    Private Sub dgv_preco_LostFocus(sender As Object, e As EventArgs) Handles dgv_preco.LostFocus
        carregar_dados_preco()
    End Sub

    Private Sub TabPage6_GotFocus(sender As Object, e As EventArgs) Handles TabPage6.GotFocus
        carregar_dados_preco()
    End Sub

    Private Sub dgv_preco_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_preco.CellContentClick
        With dgv_preco
            If .CurrentRow.Cells(3).Selected = True Then
                aux = .CurrentRow.Cells(0).Value
                resp = MsgBox("Deseja realmente excluir" + vbNewLine &
                            "A precificação: " & aux & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "AVISO")
                If resp = MsgBoxResult.Yes Then
                    SQL = "delete * from tb_preco where nome='" & aux & "'"
                    rs = db.Execute(SQL)
                    MsgBox("Excluído com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    carregar_dados_preco()
                End If
            End If
        End With

    End Sub

    Private Sub dgv_dados_estoque_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_dados_estoque.CellContentClick

        With dgv_dados_estoque

            If .CurrentRow.Cells(5).Selected = True Then
                aux_espec = .CurrentRow.Cells(1).Value
                SQL = "select * from tb_estoque where especificacoes='" & aux_espec & "'"
                rs = db.Execute(SQL)
                If rs.EOF = False Then
                    TabControl1.SelectTab(0)
                    txt_peca.Text = rs.Fields(2).Value
                    txt_espec.Text = aux_espec
                    txt_preco.Text = rs.Fields(5).Value
                    cmb_marca.SelectedText = rs.Fields(1).Value

                End If

            End If

        End With

    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btn_cadastrar.Click
        Try
            If cmb_marca.Text = "" Or txt_peca.Text = "" Or txt_qtde.Text = "" Or txt_espec.Text = "" Or txt_preco.Text = "" Then
                MsgBox("Preencha todos os campos!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "ATENÇÃO")
            Else

                SQL = "select * from tb_estoque where especificacoes= '" & txt_espec.Text & "'"
                rs = db.Execute(SQL)

                If rs.EOF = True Then

                    SQL = "insert into tb_estoque (marca,peca,disponivel,especificacoes,preco) values ('" & cmb_marca.Text & "', " &
                        " '" & txt_peca.Text & "', '" & txt_qtde.Text & "','" & txt_espec.Text & "', '" & txt_preco.Text & "') "
                    rs = db.Execute(UCase(SQL))
                    MsgBox("Produto cadastrado com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    carregar_financeiro()

                    SQL = "select * from tb_estoque where especificacoes='" & txt_espec.Text & "' and marca='" & cmb_marca.Text & "'"
                    rs = db.Execute(SQL)
                    aux_cod = rs.Fields(0).Value
                    If rs.EOF = False Then
                        SQL = "insert into tb_gasto (cod_prod,qtd,valor_unit,t_gasto,hora) values ('" & aux_cod & "', '" & txt_qtde.Text & "', " &
                            " '" & txt_preco.Text & "', '" & lbl_total.Text & "', '" & TimeString & "')"
                        rs = db.Execute(SQL)
                    End If

                    SQL = "select * from tb_marca where marca='" & cmb_marca.Text & "'"
                    rs = db.Execute(SQL)
                    If rs.EOF = True Then
                        SQL = "insert into tb_marca (marca) values ('" & cmb_marca.Text & "')"
                        rs = db.Execute(UCase(SQL))
                    End If


                    txt_peca.Clear()
                    txt_qtde.Clear()
                    txt_espec.Clear()
                    txt_preco.Clear()
                    cmb_marca.SelectedIndex = -1

                Else
                    qtd_venda = rs.Fields(3).Value
                    aux_cod = rs.Fields(0).Value

                    SQL = "insert into tb_gasto (cod_prod,qtd,valor_unit,t_gasto,hora) values ('" & aux_cod & "', '" & txt_qtde.Text & "', " &
                        " '" & txt_preco.Text & "', '" & lbl_total.Text & "', '" & TimeString & "')"
                    rs = db.Execute(SQL)
                    qtd_venda = txt_qtde.Text + qtd_venda
                    SQL = "update tb_estoque set disponivel=" & qtd_venda & ", preco= '" & txt_preco.Text & "',especificacoes= '" & txt_espec.Text & "' where ID= " & aux_cod & ""
                    rs = db.Execute(SQL)
                    carregar_dados_estoque()
                    txt_peca.Clear()
                    txt_qtde.Clear()
                    txt_espec.Clear()
                    txt_preco.Clear()
                    cmb_marca.SelectedIndex = -1



                    MsgBox("Produto atualizado ao estoque com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                    carregar_financeiro()
                End If

            End If

        Catch ex As Exception



            MsgBox("Erro ao cadastrar peça!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "AVISO")
        End Try
    End Sub



    Private Sub txt_qtde_TextChanged(sender As Object, e As EventArgs) Handles txt_qtde.TextChanged
        If txt_preco.Text <> "" And txt_qtde.Text <> "" Then
            lbl_total.Text = FormatCurrency(txt_qtde.Text * txt_preco.Text)
        Else
            lbl_total.Text = FormatCurrency(0)
        End If



    End Sub

    Private Sub txt_preco_TextChanged(sender As Object, e As EventArgs) Handles txt_preco.TextChanged
        If txt_preco.Text <> "" And txt_qtde.Text <> "" Then
            lbl_total.Text = FormatCurrency(txt_qtde.Text * txt_preco.Text)
        Else
            lbl_total.Text = FormatCurrency(0)
        End If

    End Sub


    Private Sub frm_gerente_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then

            SelectNextControl(ActiveControl, e.Shift, True, True, True)
        End If
    End Sub


    Private Sub PictureBox3_MouseHover(sender As Object, e As EventArgs) Handles PictureBox3.MouseHover
        ToolTip1.Show("Por exemplo: Flex, Fone", PictureBox3)
    End Sub

    Private Sub PictureBox2_MouseHover(sender As Object, e As EventArgs) Handles PictureBox2.MouseHover
        ToolTip1.Show("Por exemplo: Flex A30, Fone Buds Live", PictureBox2)
    End Sub


    Private Sub txt_busca_TextChanged(sender As Object, e As EventArgs) Handles txt_busca.TextChanged
        Try
            If cmb_busca.Text = "EM FALTA" Then

                If txt_busca.Text = "" Then
                    SQL = "select * from tb_estoque where disponivel < 10"
                    rs = db.Execute(SQL)

                    With dgv_dados_estoque
                        .Rows.Clear()
                        Do While rs.EOF = False
                            .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                            rs.MoveNext()

                        Loop
                    End With
                Else
                    SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%' and disponivel < 10"
                    rs = db.Execute(SQL)

                    With dgv_dados_estoque
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

                With dgv_dados_estoque
                    .Rows.Clear()
                    Do While rs.EOF = False
                        .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                        rs.MoveNext()

                    Loop
                End With
            ElseIf cmb_busca.Text = "TODOS" Then
                SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%'"
                rs = db.Execute(SQL)

                With dgv_dados_estoque
                    .Rows.Clear()
                    Do While rs.EOF = False
                        .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                        rs.MoveNext()

                    Loop
                End With
            Else

                SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%' and marca='" & cmb_busca.Text & "'"
                rs = db.Execute(SQL)

                With dgv_dados_estoque
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

    Private Sub btn_busca_Click(sender As Object, e As EventArgs) Handles btn_busca.Click
        If cmb_busca.Text = "EM FALTA" Then

            If txt_busca.Text = "" Then
                SQL = "select * from tb_estoque where disponivel < 10"
                rs = db.Execute(SQL)

                With dgv_dados_estoque
                    .Rows.Clear()
                    Do While rs.EOF = False
                        .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                        rs.MoveNext()

                    Loop
                End With
            Else
                SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%' and disponivel < 10"
                rs = db.Execute(SQL)

                With dgv_dados_estoque
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

            With dgv_dados_estoque
                .Rows.Clear()
                Do While rs.EOF = False
                    .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                    rs.MoveNext()

                Loop
            End With

        ElseIf cmb_busca.Text = "TODOS" Then
            SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%'"
            rs = db.Execute(SQL)

            With dgv_dados_estoque
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

                With dgv_dados_estoque
                    .Rows.Clear()
                    Do While rs.EOF = False
                        .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                        rs.MoveNext()

                    Loop
                End With
            Else
                SQL = "select * from tb_estoque where especificacoes like '" & txt_busca.Text & "%' and marca='" & cmb_busca.Text & "'"
                rs = db.Execute(SQL)

                With dgv_dados_estoque
                    .Rows.Clear()
                    Do While rs.EOF = False
                        .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value), Nothing)
                        rs.MoveNext()

                    Loop
                End With
            End If
        End If
    End Sub



    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles txt_pesquisa.TextChanged
        Try
            SQL = "select * from tb_acesso where " & cmb_tipo.Text & " like '" & txt_pesquisa.Text & "%'"
            rs = db.Execute(SQL)

            With dgv_dados
                .Rows.Clear()
                Do While rs.EOF = False
                    .Rows.Add(rs.Fields(2).Value, rs.Fields(1).Value, rs.Fields(3).Value, rs.Fields(4).Value, rs.Fields(6).Value)
                    rs.MoveNext()

                Loop
            End With
        Catch ex As Exception
            MsgBox("Erro ao carregar dados", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Private Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        Try
            SQL = "select * from tb_acesso where " & cmb_tipo.Text & " like '" & txt_pesquisa.Text & "%'"
            rs = db.Execute(SQL)

            With dgv_dados
                .Rows.Clear()
                Do While rs.EOF = False

                    .Rows.Add(rs.Fields(2).Value, rs.Fields(1).Value, rs.Fields(3).Value, rs.Fields(4).Value, rs.Fields(6).Value)
                    rs.MoveNext()

                Loop
            End With
        Catch ex As Exception
            MsgBox("Erro ao carregar dados", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub


    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Process.Start(Application.StartupPath & "\Banco_Dados\cadastro.mdb")
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Process.Start(Application.StartupPath & "\Banco_Dados\cadastro.mdb")
    End Sub

    Private Sub Relatórios_Click(sender As Object, e As EventArgs) Handles Relatórios.Click
        Process.Start(Application.StartupPath & "\Banco_Dados\cadastro.mdb")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Process.Start(Application.StartupPath & "\Banco_Dados\cadastro.mdb")
    End Sub

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover
        ToolTip1.Show("Clique para verificar os relatórios!!", PictureBox1)
    End Sub


End Class