﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MyGridView1 = New MyControl.MyGridView
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'MyGridView1
        '
        Me.MyGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyGridView1.Caption = "DANH SÁCH NHÓM NGƯỜI SỬ DỤNG"
        Me.MyGridView1.DataSource = Nothing
        Me.MyGridView1.IsReadOnly = False
        Me.MyGridView1.Location = New System.Drawing.Point(5, 5)
        Me.MyGridView1.Name = "MyGridView1"
        Me.MyGridView1.NumberRow = 0
        Me.MyGridView1.Size = New System.Drawing.Size(847, 300)
        Me.MyGridView1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(335, 311)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(74, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 338)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.MyGridView1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MyGridView1 As MyControl.MyGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
