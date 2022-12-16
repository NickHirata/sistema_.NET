<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_esqueceu
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_esqueceu))
        Me.txt_email = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_redefinir = New System.Windows.Forms.Button()
        Me.txt_csenha = New System.Windows.Forms.TextBox()
        Me.txt_nsenha = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_cpf = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbl_p = New System.Windows.Forms.LinkLabel()
        Me.visu = New System.Windows.Forms.PictureBox()
        CType(Me.visu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txt_email
        '
        Me.txt_email.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_email.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_email.Location = New System.Drawing.Point(35, 199)
        Me.txt_email.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txt_email.Name = "txt_email"
        Me.txt_email.Size = New System.Drawing.Size(273, 27)
        Me.txt_email.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.Label2.Location = New System.Drawing.Point(365, 180)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 21)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "CPF"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.Label1.Location = New System.Drawing.Point(31, 180)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 21)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "E-MAIL"
        '
        'btn_redefinir
        '
        Me.btn_redefinir.Cursor = System.Windows.Forms.Cursors.Default
        Me.btn_redefinir.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btn_redefinir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_redefinir.Location = New System.Drawing.Point(195, 413)
        Me.btn_redefinir.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_redefinir.Name = "btn_redefinir"
        Me.btn_redefinir.Size = New System.Drawing.Size(166, 39)
        Me.btn_redefinir.TabIndex = 5
        Me.btn_redefinir.Text = "REDEFINIR"
        Me.btn_redefinir.UseVisualStyleBackColor = True
        '
        'txt_csenha
        '
        Me.txt_csenha.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_csenha.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_csenha.Location = New System.Drawing.Point(339, 318)
        Me.txt_csenha.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txt_csenha.Name = "txt_csenha"
        Me.txt_csenha.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txt_csenha.Size = New System.Drawing.Size(168, 27)
        Me.txt_csenha.TabIndex = 4
        '
        'txt_nsenha
        '
        Me.txt_nsenha.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_nsenha.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_nsenha.Location = New System.Drawing.Point(35, 318)
        Me.txt_nsenha.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txt_nsenha.Name = "txt_nsenha"
        Me.txt_nsenha.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.txt_nsenha.Size = New System.Drawing.Size(168, 27)
        Me.txt_nsenha.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.Label3.Location = New System.Drawing.Point(335, 295)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 21)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Confirmar senha"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.Label4.Location = New System.Drawing.Point(31, 295)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 21)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Nova senha"
        '
        'txt_cpf
        '
        Me.txt_cpf.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_cpf.Location = New System.Drawing.Point(369, 203)
        Me.txt_cpf.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txt_cpf.Mask = "999,999,999-99"
        Me.txt_cpf.Name = "txt_cpf"
        Me.txt_cpf.Size = New System.Drawing.Size(131, 22)
        Me.txt_cpf.TabIndex = 2
        Me.txt_cpf.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label5.Font = New System.Drawing.Font("Segoe Script", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label5.Location = New System.Drawing.Point(134, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(316, 50)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Esqueceu a senha?"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(152, 86)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(285, 63)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Informe seu e-mail e seu CPF," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "para começar o processo de redefinição" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "de senha!!" &
    "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lbl_p
        '
        Me.lbl_p.AutoSize = True
        Me.lbl_p.BackColor = System.Drawing.Color.Transparent
        Me.lbl_p.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbl_p.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_p.LinkColor = System.Drawing.Color.Red
        Me.lbl_p.Location = New System.Drawing.Point(262, 250)
        Me.lbl_p.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_p.Name = "lbl_p"
        Me.lbl_p.Size = New System.Drawing.Size(245, 17)
        Me.lbl_p.TabIndex = 23
        Me.lbl_p.TabStop = True
        Me.lbl_p.Text = "Preencha os campos para prosseguir"
        '
        'visu
        '
        Me.visu.BackColor = System.Drawing.Color.Gainsboro
        Me.visu.Cursor = System.Windows.Forms.Cursors.Default
        Me.visu.Image = CType(resources.GetObject("visu.Image"), System.Drawing.Image)
        Me.visu.Location = New System.Drawing.Point(513, 325)
        Me.visu.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.visu.Name = "visu"
        Me.visu.Size = New System.Drawing.Size(29, 20)
        Me.visu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.visu.TabIndex = 24
        Me.visu.TabStop = False
        '
        'frm_esqueceu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ClientSize = New System.Drawing.Size(586, 501)
        Me.Controls.Add(Me.visu)
        Me.Controls.Add(Me.lbl_p)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_cpf)
        Me.Controls.Add(Me.txt_csenha)
        Me.Controls.Add(Me.txt_nsenha)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btn_redefinir)
        Me.Controls.Add(Me.txt_email)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frm_esqueceu"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "REDEFINIR SENHA"
        CType(Me.visu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt_email As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btn_redefinir As Button
    Friend WithEvents txt_csenha As TextBox
    Friend WithEvents txt_nsenha As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_cpf As MaskedTextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lbl_p As LinkLabel
    Friend WithEvents visu As PictureBox
End Class
