Public Class frm_login

    'A form with custom border and title bar.
    'Some functions, such as resize the window via mouse, are not implemented yet. 


    'The color and the width of the border.
    Private borderColor As Color = ColorTranslator.FromHtml("#ff6200")
    Private borderWidth As Integer = 3
    'The color and region of the header.
    Private headerColor As Color = ColorTranslator.FromHtml("#ff6200")
    Private headerRect As Rectangle
    'The region of the client.
    Private clientRect As Rectangle
    'The region of the title text.
    Private titleRect As Rectangle
    'The region of the minimum button.
    Private miniBoxRect As Rectangle
    'The region of the maximum button.
    Private maxBoxRect As Rectangle
    'The region of the close button.
    Private closeBoxRect As Rectangle
    'The states of the three header buttons.
    Private miniState As ButtonState
    Private maxState As ButtonState
    Private closeState As ButtonState
    'Store the mouse down point to handle moving the form.
    Private x As Integer = 0
    Private y As Integer = 0
    'The height of the header.
    Const HEADER_HEIGHT As Integer = 25
    'The size of the header buttons.
    ReadOnly BUTTON_BOX_SIZE As Size = New Size(15, 15)

    Private Sub CustomBorderColorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Hide the border and the title bar.
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
    End Sub

    Private Sub CustomBorderColorForm_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        'Draw the header.
        Using b As Brush = New SolidBrush(borderColor)
            e.Graphics.FillRectangle(b, headerRect)
        End Using
        'Draw the title text
        Using b As Brush = New SolidBrush(Me.ForeColor)
            e.Graphics.DrawString(Me.Text, Me.Font, b, titleRect)
        End Using
        'Draw the header buttons.
        If Me.MinimizeBox Then
            ControlPaint.DrawCaptionButton(e.Graphics, miniBoxRect, CaptionButton.Minimize, miniState)
        End If
        If Me.MinimizeBox Then
            ControlPaint.DrawCaptionButton(e.Graphics, maxBoxRect, CaptionButton.Maximize, maxState)
        End If
        If Me.MinimizeBox Then
            ControlPaint.DrawCaptionButton(e.Graphics, closeBoxRect, CaptionButton.Close, closeState)
        End If
        'Draw the border.
        ControlPaint.DrawBorder(e.Graphics, clientRect, borderColor,
            borderWidth, ButtonBorderStyle.Solid, borderColor, borderWidth, ButtonBorderStyle.Solid, borderColor, borderWidth, ButtonBorderStyle.Solid, borderColor, borderWidth, ButtonBorderStyle.Solid)
    End Sub

    'Handle resize to adjust the region ot border, header and so on.
    Private Sub CustomBorderColorForm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        headerRect = New Rectangle(Me.ClientRectangle.Location, New Size(Me.ClientRectangle.Width, HEADER_HEIGHT))
        clientRect = New Rectangle(New Point(Me.ClientRectangle.Location.X, Me.ClientRectangle.Y + HEADER_HEIGHT),
            New Point(Me.ClientRectangle.Width, Me.ClientRectangle.Height - HEADER_HEIGHT))
        Dim yOffset = (headerRect.Height + borderWidth - BUTTON_BOX_SIZE.Height) / 2
        titleRect = New Rectangle(yOffset, yOffset,
                                Me.ClientRectangle.Width - 3 * (BUTTON_BOX_SIZE.Width + 1) - yOffset,
                                BUTTON_BOX_SIZE.Height)
        miniBoxRect = New Rectangle(Me.ClientRectangle.Width - 3 * (BUTTON_BOX_SIZE.Width + 1),
                                    yOffset, BUTTON_BOX_SIZE.Width, BUTTON_BOX_SIZE.Height)
        maxBoxRect = New Rectangle(Me.ClientRectangle.Width - 2 * (BUTTON_BOX_SIZE.Width + 1),
                                    yOffset, BUTTON_BOX_SIZE.Width, BUTTON_BOX_SIZE.Height)
        closeBoxRect = New Rectangle(Me.ClientRectangle.Width - 1 * (BUTTON_BOX_SIZE.Width + 1),
                                    yOffset, BUTTON_BOX_SIZE.Width, BUTTON_BOX_SIZE.Height)
        Me.Invalidate()
    End Sub


    Private Sub CustomBorderColorForm_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        'Start to move the form.
        If (titleRect.Contains(e.Location)) Then
            x = e.X
            y = e.Y
        End If

        'Check and press the header buttons.
        Dim mousePos As Point = Me.PointToClient(Control.MousePosition)
        If (miniBoxRect.Contains(mousePos)) Then
            miniState = ButtonState.Pushed
        ElseIf (maxBoxRect.Contains(mousePos)) Then
            maxState = ButtonState.Pushed
        ElseIf (closeBoxRect.Contains(mousePos)) Then
            closeState = ButtonState.Pushed
        End If

    End Sub

    Private Sub CustomBorderColorForm_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        'Move and refresh.
        If (x <> 0 And y <> 0) Then
            Me.Location = New Point(Me.Left + e.X - x, Me.Top + e.Y - y)
            Me.Refresh()
        End If

    End Sub

    Private Sub CustomBorderColorForm_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        'Reset the mouse point.
        x = 0
        y = 0

        'Check the button states and modify the window state.
        If miniState = ButtonState.Pushed Then
            Me.WindowState = FormWindowState.Minimized
            miniState = ButtonState.Normal
        ElseIf maxState = ButtonState.Pushed Then
            If Me.WindowState = FormWindowState.Normal Then
                Me.WindowState = FormWindowState.Maximized
                maxState = ButtonState.Checked
            Else
                Me.WindowState = FormWindowState.Normal
                maxState = ButtonState.Normal
            End If
        ElseIf closeState = ButtonState.Pushed Then
            Me.Close()
        End If

    End Sub

    'Handle this event to maxmize/normalize the form via double clicking the title bar.
    Private Sub CustomBorderColorForm_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDoubleClick
        If (titleRect.Contains(e.Location)) Then
            If Me.WindowState = FormWindowState.Normal Then
                Me.WindowState = FormWindowState.Maximized
                maxState = ButtonState.Checked
            Else
                Me.WindowState = FormWindowState.Normal
                maxState = ButtonState.Normal
            End If
        End If
    End Sub
    Private Sub chk_visu_CheckedChanged(sender As Object, e As EventArgs) Handles chk_visu.CheckedChanged
        Try
            If chk_visu.Checked = True Then
                txt_senha.PasswordChar = ""
            Else
                txt_senha.PasswordChar = "•"
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_entrar.Click
        Try
            If txt_user.Text = "" Or txt_senha.Text = "" Then
                MsgBox("Preencha todos os campos!", MsgBoxStyle.Information + vbOKOnly, "ATENÇÃO")
            Else

                SQL = "select * from tb_login where email=  '" & txt_user.Text & "'  And senha='" & txt_senha.Text & "' "
                rs = db.Execute(SQL)
                If rs.EOF = False Then
                    carg = rs.Fields(4).Value
                    status = rs.Fields(6).Value
                    nome = UCase(rs.Fields(1).Value)
                    If carg = "Administrador" Then
                        MsgBox("Conta logada com sucesso", MsgBoxStyle.Information + vbOKOnly, "ATENÇÃO")
                        SQL = "insert into tb_acesso  (email , nome, entrada, saida, cargo ) values ( '" & txt_user.Text & "', " &
                        " '" & nome & "', '" & TimeOfDay & "','ATIVO AGORA', '" & carg & "') "
                        rs = db.Execute(SQL)
                        acesso = TimeOfDay
                        Form1.ShowDialog()
                        carregar_dados()
                        Me.Dispose()

                    ElseIf carg = "Gerente" Then

                        If status = "ATIVA" Then
                            MsgBox("Conta logada com sucesso", MsgBoxStyle.Information + vbOKOnly, "ATENÇÃO")
                            SQL = "insert into tb_acesso  (email , nome, entrada, saida, cargo ) values ( '" & txt_user.Text & "', " &
                        " '" & nome & "', '" & TimeOfDay & "','ATIVO AGORA', '" & carg & "') "
                            rs = db.Execute(SQL)
                            acesso = TimeOfDay
                            frm_gerente.ShowDialog()
                            carregar_dados()
                            Me.Dispose()
                        Else
                            MsgBox("Conta bloqueada!" + vbNewLine &
                                   "Contate o administrador!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
                            txt_senha.Clear()
                            txt_user.Clear()
                            txt_user.Focus()
                        End If

                    ElseIf carg = "Vendedor" Then

                        If status = "ATIVA" Then
                            MsgBox("Conta logada com sucesso", MsgBoxStyle.Information + vbOKOnly, "ATENÇÃO")
                            SQL = "insert into tb_acesso  (email , nome, entrada, saida, cargo ) values ( '" & txt_user.Text & "', " &
                        " '" & nome & "', '" & TimeOfDay & "','ATIVO AGORA', '" & carg & "') "
                            rs = db.Execute(SQL)
                            acesso = TimeOfDay
                            frm_vendedor.ShowDialog()
                            Me.Dispose()
                        Else
                            MsgBox("Conta bloqueada!" + vbNewLine &
                                   "Contate o administrador!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
                        End If

                    End If



                Else
                    MsgBox("Login inválido", MsgBoxStyle.Critical + vbOKOnly, "ATENÇÃO")
                    lbl_esqueceu.Visible = True
                    txt_senha.Clear()
                    txt_user.Clear()
                    txt_user.Focus()
                End If




            End If
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub

    Private Sub frm_login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conecta_banco()
        lbl_esqueceu.Visible = False
        lbl_esqueceu.ForeColor = ColorTranslator.FromHtml("#fcddcc")
        btn_entrar.ForeColor = ColorTranslator.FromHtml("#fcddcc")
        txt_senha.BackColor = ColorTranslator.FromHtml("#2a2a32")
        txt_user.BackColor = ColorTranslator.FromHtml("#2a2a32")
        txt_senha.ForeColor = ColorTranslator.FromHtml("#d5d5db")
        txt_user.ForeColor = ColorTranslator.FromHtml("#d5d5db")
    End Sub

    Private Sub lbl_esqueceu_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbl_esqueceu.LinkClicked
        Me.Hide()
        frm_esqueceu.ShowDialog()


    End Sub

    Private Sub frm_login_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then

            SelectNextControl(ActiveControl, e.Shift, True, True, True)
        End If
    End Sub
End Class