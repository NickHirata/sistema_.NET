Module Module1
    Public diretorio, SQL, aux_email, aux_cpf, aux_prod, saida As String
    Public carg, nome, status, resp, aux, marca, texto, acesso As String
    Public db As New ADODB.Connection 'Variavel de BD
    Public rs As New ADODB.Recordset 'Variavel de TB
    Public dirbanco = Application.StartupPath & "\Banco_Dados\cadastro.mdb"
    Public cont, aux_stts As Integer
    Public aux_venda, qtd_venda, aux_preco, aux_cod, aux_cliente As Integer
    Public aux_desc, aux_lucro As Double
    Public aux_espec As String
    Public aux_estoque, total_linhas, aux_dvenda As Integer
    Public data_hoje As DateTime


    Sub mostra_texto(objeto As Control, mensagem As String)

        frm_gerente.ToolTip1.Show(mensagem, objeto)
    End Sub


    Sub tirar_estoque()
        With frm_vendedor


            For Each linha As DataGridViewRow In .dgv_carrinho.Rows
                total_linhas = total_linhas + 1

                qtd_venda = 0
                aux_estoque = 0
                aux_cod = linha.Cells(0).Value
                aux_prod = linha.Cells(5).Value
                SQL = "select * from tb_estoque where ID=" & aux_cod & ""
                rs = db.Execute(SQL)
                If rs.EOF = False Then
                    aux_estoque = rs.Fields(3).Value
                    qtd_venda = linha.Cells(3).Value
                    aux_estoque = aux_estoque - qtd_venda
                    SQL = "update tb_estoque set disponivel=" & aux_estoque & " where ID=" & aux_cod & ""
                    rs = db.Execute(SQL)

                End If


                carregar_dados_estoque()
            Next




        End With
    End Sub
    Sub carregar_lbl_preco()
        With frm_vendedor

            carregar_carrinho()
            SQL = "select * from tb_preco where nome= '" & .cmb_preco.Text & "'"
            rs = db.Execute(SQL)
            If rs.EOF = False Then
                aux_lucro = ((rs.Fields(2).Value / 100) + 1)
                aux_desc = rs.Fields(3).Value / 100

            End If
            Dim total_vendas As Double = 0.00

            For Each linha As DataGridViewRow In .dgv_carrinho.Rows
                total_vendas = total_vendas + linha.Cells(1).Value
                total_linhas = total_linhas + 1

            Next



            .lbl_vt.Text = FormatCurrency(total_vendas)
            .lbl_desc.Text = FormatCurrency(aux_desc * total_vendas)
            .lbl_vf.Text = FormatCurrency((total_vendas) - aux_desc * total_vendas)


            carregar_carrinho()
            total_vendas = 0.00
        End With

    End Sub

    Sub conecta_banco()

        Try
            'String de Conexão com o Banco de Dados - MSAccess
            db = CreateObject("ADODB.Connection") 'Instanciar o banco na variavel
            db.Open("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" & dirbanco)

        Catch ex As Exception
            MsgBox("Erro ao conectar com o banco de dados", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Sub carregar_financeiro()
        SQL = "select * from tb_gasto"
        rs = db.Execute(SQL)
        With frm_gerente.dgv_gasto
            .Rows.Clear()
            Do While rs.EOF = False
                .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value)
                rs.MoveNext()
            Loop
        End With
        SQL = "select * from tb_venda_total"
        rs = db.Execute(SQL)
        With frm_gerente.dgv_venda
            .Rows.Clear()
            Do While rs.EOF = False
                .Rows.Add(rs.Fields(0).Value, rs.Fields(3).Value)
                rs.MoveNext()
            Loop
        End With
        carregar_total()

    End Sub

    Sub carregar_total()

        With frm_gerente

            Dim total_vendas As Double = 0.00
            Dim total_compras As Double = 0.00

            For Each linha As DataGridViewRow In .dgv_venda.Rows
                total_vendas = total_vendas + linha.Cells(1).Value


            Next
            For Each linha As DataGridViewRow In .dgv_gasto.Rows
                total_compras = total_compras + linha.Cells(1).Value

            Next

            .lbl_gasto.Text = FormatCurrency(total_compras)
            .lbl_gasto.ForeColor = ColorTranslator.FromHtml("#FF0000")
            .lbl_recebido.Text = FormatCurrency(total_vendas)
            .lbl_recebido.ForeColor = ColorTranslator.FromHtml("#32CD32")
            If total_compras > total_vendas Then
                .lbl_lucro.ForeColor = ColorTranslator.FromHtml("#FF0000")
            Else
                .lbl_lucro.ForeColor = ColorTranslator.FromHtml("#32CD32")
            End If

            .lbl_lucro.Text = FormatCurrency(total_vendas - total_compras)
        End With

    End Sub
    Sub carregar_carrinho()


        SQL = "select * from tb_vendas where id_cliente=" & aux_cliente & " and status='RASCUNHO'"
        rs = db.Execute(SQL)
        If rs.EOF = False Then
            With frm_vendedor
                SQL = "select * from tb_preco where nome= '" & .cmb_preco.Text & "'"
                rs = db.Execute(SQL)
                If rs.EOF = False Then
                    aux_lucro = ((rs.Fields(2).Value / 100) + 1)
                    aux_desc = rs.Fields(3).Value / 100

                End If

                SQL = "select * from tb_vendas where id_cliente=" & aux_cliente & " and status='RASCUNHO'"
                rs = db.Execute(SQL)
                .dgv_carrinho.Rows.Clear()
                Do While rs.EOF = False
                    .dgv_carrinho.Rows.Add(rs.Fields(2).Value, FormatCurrency(rs.Fields(4).Value * rs.Fields(3).Value * aux_lucro), Nothing, rs.Fields(3).Value, Nothing, rs.Fields(8).Value)
                    rs.MoveNext()
                Loop
            End With
        Else
            frm_vendedor.dgv_carrinho.Rows.Clear()

        End If

    End Sub
    Sub carregar_dados_preco()
        SQL = "select * from tb_preco order by nome asc"
        rs = db.Execute(SQL)
        With frm_gerente.dgv_preco
            .Rows.Clear()
            Do While rs.EOF = False
                .Rows.Add(rs.Fields(1).Value, rs.Fields(2).Value, rs.Fields(3).Value, Nothing)
                rs.MoveNext()
            Loop
        End With


    End Sub
    Sub carregar_dados_adm()
        Try
            SQL = "select * from tb_login order by nome asc"
            rs = db.Execute(SQL)

            With Form1.dgv_dados
                .Rows.Clear()
                Do While rs.EOF = False
                    .Rows.Add(rs.Fields(1).Value, rs.Fields(2).Value, rs.Fields(3).Value, rs.Fields(4).Value, rs.Fields(6).Value)
                    rs.MoveNext()

                Loop
            End With

        Catch ex As Exception
            MsgBox("Erro ao carregar dados", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub
    Sub carregar_gerencial()
        Try
            SQL = "select * from tb_vendas order by vendedor asc"
            rs = db.Execute(SQL)
            With frm_gerente.dgv_geren
                .Rows.Clear()
                Do While rs.EOF = False
                    .Rows.Add(rs.Fields(2).Value, rs.Fields(1).Value, rs.Fields(3).Value, rs.Fields(4).Value * rs.Fields(3).Value, rs.Fields(7).Value)
                    rs.MoveNext()
                Loop

            End With
            SQL = "select * from tb_vendas order by vendedor asc"
            rs = db.Execute(SQL)
            With Form1.dgv_gerencial
                .Rows.Clear()
                Do While rs.EOF = False
                    .Rows.Add(rs.Fields(2).Value, rs.Fields(1).Value, rs.Fields(3).Value, rs.Fields(4).Value * rs.Fields(3).Value, rs.Fields(7).Value)
                    rs.MoveNext()
                Loop

            End With

            SQL = "select * from tb_vendas order by vendedor asc"
            rs = db.Execute(SQL)
            With frm_vendedor.dgv_geren
                .Rows.Clear()
                Do While rs.EOF = False
                    .Rows.Add(rs.Fields(2).Value, rs.Fields(1).Value, rs.Fields(3).Value, rs.Fields(4).Value * rs.Fields(3).Value, rs.Fields(7).Value)
                    rs.MoveNext()
                Loop

            End With


        Catch ex As Exception

        End Try
    End Sub
    Sub carregar_dados_estoque()
        Try
            SQL = "select * from tb_estoque order by marca asc"
            rs = db.Execute(SQL)
            With frm_vendedor.dgv_estoque
                .Rows.Clear()
                Do While rs.EOF = False
                    .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value))
                    rs.MoveNext()
                Loop
            End With
            SQL = "select * from tb_estoque order by marca asc"
            rs = db.Execute(SQL)
            With frm_gerente.dgv_dados_estoque
                .Rows.Clear()
                Do While rs.EOF = False
                    .Rows.Add(rs.Fields(0).Value, rs.Fields(4).Value, rs.Fields(1).Value, rs.Fields(3).Value, FormatCurrency(rs.Fields(5).Value))
                    rs.MoveNext()
                Loop

            End With
        Catch ex As Exception

        End Try
    End Sub
    Sub carregar_dados()
        Try
            SQL = "select * from tb_acesso order by nome asc"
            rs = db.Execute(SQL)
            cont = 1
            With frm_gerente.dgv_dados
                .Rows.Clear()
                Do While rs.EOF = False


                    .Rows.Add(rs.Fields(2).Value, rs.Fields(1).Value, rs.Fields(3).Value, rs.Fields(4).Value, rs.Fields(6).Value)
                    rs.MoveNext()
                    cont = cont + 1

                Loop
            End With

        Catch ex As Exception
            MsgBox("Erro ao carregar dados", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Sub carregar_marcas()
        SQL = "select * from tb_marca marca"
        rs = db.Execute(SQL)
        With frm_gerente
            .cmb_marca.Items.Clear()
            Do While rs.EOF = False
                .cmb_marca.Items.Add(rs.Fields(1).Value)
                rs.MoveNext()

            Loop
        End With
    End Sub

End Module
