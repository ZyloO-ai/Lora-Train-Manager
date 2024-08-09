<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(Form1))
        TreeView1 = New TreeView()
        BtnPending = New Button()
        BtnDone = New Button()
        BtnRetrain = New Button()
        BtnNew = New Button()
        BtnDelete = New Button()
        Label1 = New Label()
        LabelImgCount = New Label()
        Label2 = New Label()
        LabelHasCaptions = New Label()
        Label3 = New Label()
        LabelHasModel = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        BtnOpenFolder = New Button()
        BtnCopyPath = New Button()
        BtnOpenKohya = New Button()
        BtnOpenTagger = New Button()
        BtnCalcSteps = New Button()
        BtnChangeRootFolder = New Button()
        BtnChangeKohyaFolder = New Button()
        BtnChangeTaggerFolder = New Button()
        BtnUpdate = New Button()
        underline = New Label()
        LabelSelectedFolder = New Label()
        SuspendLayout()
        ' 
        ' TreeView1
        ' 
        TreeView1.BackColor = Color.Black
        TreeView1.Font = New Font("Segoe UI", 12F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point)
        TreeView1.ForeColor = SystemColors.Info
        TreeView1.Location = New Point(13, 49)
        TreeView1.Margin = New Padding(4)
        TreeView1.Name = "TreeView1"
        TreeView1.Size = New Size(310, 659)
        TreeView1.TabIndex = 0
        ' 
        ' BtnPending
        ' 
        BtnPending.BackColor = Color.FromArgb(CByte(255), CByte(128), CByte(128))
        BtnPending.FlatStyle = FlatStyle.Flat
        BtnPending.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnPending.Location = New Point(328, 135)
        BtnPending.Name = "BtnPending"
        BtnPending.Size = New Size(83, 36)
        BtnPending.TabIndex = 1
        BtnPending.Text = "Pending"
        BtnPending.UseVisualStyleBackColor = False
        ' 
        ' BtnDone
        ' 
        BtnDone.BackColor = Color.FromArgb(CByte(128), CByte(255), CByte(128))
        BtnDone.FlatStyle = FlatStyle.Flat
        BtnDone.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnDone.Location = New Point(417, 135)
        BtnDone.Name = "BtnDone"
        BtnDone.Size = New Size(83, 36)
        BtnDone.TabIndex = 2
        BtnDone.Text = "Done"
        BtnDone.UseVisualStyleBackColor = False
        ' 
        ' BtnRetrain
        ' 
        BtnRetrain.BackColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        BtnRetrain.FlatStyle = FlatStyle.Flat
        BtnRetrain.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnRetrain.Location = New Point(506, 135)
        BtnRetrain.Name = "BtnRetrain"
        BtnRetrain.Size = New Size(83, 36)
        BtnRetrain.TabIndex = 3
        BtnRetrain.Text = "Retrain"
        BtnRetrain.UseVisualStyleBackColor = False
        ' 
        ' BtnNew
        ' 
        BtnNew.BackColor = Color.PaleGreen
        BtnNew.FlatStyle = FlatStyle.Flat
        BtnNew.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnNew.Location = New Point(13, 10)
        BtnNew.Name = "BtnNew"
        BtnNew.Size = New Size(116, 32)
        BtnNew.TabIndex = 4
        BtnNew.Text = "New"
        BtnNew.UseVisualStyleBackColor = False
        ' 
        ' BtnDelete
        ' 
        BtnDelete.BackColor = Color.IndianRed
        BtnDelete.FlatStyle = FlatStyle.Flat
        BtnDelete.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnDelete.Location = New Point(135, 10)
        BtnDelete.Name = "BtnDelete"
        BtnDelete.Size = New Size(127, 31)
        BtnDelete.TabIndex = 5
        BtnDelete.Text = "Delete"
        BtnDelete.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 14.25F, FontStyle.Italic, GraphicsUnit.Point)
        Label1.ForeColor = SystemColors.ButtonFace
        Label1.Location = New Point(350, 232)
        Label1.Name = "Label1"
        Label1.Size = New Size(121, 25)
        Label1.TabIndex = 6
        Label1.Text = "Image Count:"
        ' 
        ' LabelImgCount
        ' 
        LabelImgCount.AutoSize = True
        LabelImgCount.BackColor = Color.Transparent
        LabelImgCount.Font = New Font("Segoe UI", 14.25F, FontStyle.Italic, GraphicsUnit.Point)
        LabelImgCount.ForeColor = SystemColors.ButtonFace
        LabelImgCount.Location = New Point(478, 232)
        LabelImgCount.Name = "LabelImgCount"
        LabelImgCount.Size = New Size(22, 25)
        LabelImgCount.TabIndex = 7
        LabelImgCount.Text = "0"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 14.25F, FontStyle.Italic, GraphicsUnit.Point)
        Label2.ForeColor = SystemColors.ButtonFace
        Label2.Location = New Point(350, 268)
        Label2.Name = "Label2"
        Label2.Size = New Size(124, 25)
        Label2.TabIndex = 8
        Label2.Text = "Has Captions:"
        ' 
        ' LabelHasCaptions
        ' 
        LabelHasCaptions.AutoSize = True
        LabelHasCaptions.BackColor = Color.Transparent
        LabelHasCaptions.Font = New Font("Segoe UI", 14.25F, FontStyle.Italic, GraphicsUnit.Point)
        LabelHasCaptions.ForeColor = SystemColors.ButtonFace
        LabelHasCaptions.Location = New Point(478, 268)
        LabelHasCaptions.Name = "LabelHasCaptions"
        LabelHasCaptions.Size = New Size(36, 25)
        LabelHasCaptions.TabIndex = 9
        LabelHasCaptions.Text = "No"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 14.25F, FontStyle.Italic, GraphicsUnit.Point)
        Label3.ForeColor = SystemColors.ButtonFace
        Label3.Location = New Point(350, 306)
        Label3.Name = "Label3"
        Label3.Size = New Size(102, 25)
        Label3.TabIndex = 10
        Label3.Text = "Has Model:"
        ' 
        ' LabelHasModel
        ' 
        LabelHasModel.AutoSize = True
        LabelHasModel.BackColor = Color.Transparent
        LabelHasModel.Font = New Font("Segoe UI", 14.25F, FontStyle.Italic, GraphicsUnit.Point)
        LabelHasModel.ForeColor = SystemColors.ButtonFace
        LabelHasModel.Location = New Point(478, 306)
        LabelHasModel.Name = "LabelHasModel"
        LabelHasModel.Size = New Size(36, 25)
        LabelHasModel.TabIndex = 11
        LabelHasModel.Text = "No"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Segoe UI Semibold", 14.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point)
        Label4.ForeColor = SystemColors.ButtonFace
        Label4.Location = New Point(330, 101)
        Label4.Name = "Label4"
        Label4.Size = New Size(99, 25)
        Label4.TabIndex = 12
        Label4.Text = "| Set State"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BackColor = Color.Transparent
        Label5.Font = New Font("Segoe UI Semibold", 14.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point)
        Label5.ForeColor = SystemColors.ButtonFace
        Label5.Location = New Point(330, 197)
        Label5.Name = "Label5"
        Label5.Size = New Size(56, 25)
        Label5.TabIndex = 13
        Label5.Text = "| Info"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Segoe UI Semibold", 14.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point)
        Label6.ForeColor = SystemColors.ButtonFace
        Label6.Location = New Point(330, 352)
        Label6.Name = "Label6"
        Label6.Size = New Size(60, 25)
        Label6.TabIndex = 14
        Label6.Text = "| Utils"
        ' 
        ' BtnOpenFolder
        ' 
        BtnOpenFolder.BackColor = SystemColors.ButtonFace
        BtnOpenFolder.FlatStyle = FlatStyle.Flat
        BtnOpenFolder.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnOpenFolder.Location = New Point(369, 393)
        BtnOpenFolder.Name = "BtnOpenFolder"
        BtnOpenFolder.Size = New Size(178, 36)
        BtnOpenFolder.TabIndex = 15
        BtnOpenFolder.Text = "Open Folder"
        BtnOpenFolder.UseVisualStyleBackColor = False
        ' 
        ' BtnCopyPath
        ' 
        BtnCopyPath.BackColor = SystemColors.ButtonFace
        BtnCopyPath.FlatStyle = FlatStyle.Flat
        BtnCopyPath.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnCopyPath.Location = New Point(369, 445)
        BtnCopyPath.Name = "BtnCopyPath"
        BtnCopyPath.Size = New Size(178, 36)
        BtnCopyPath.TabIndex = 16
        BtnCopyPath.Text = "Copy Path"
        BtnCopyPath.UseVisualStyleBackColor = False
        ' 
        ' BtnOpenKohya
        ' 
        BtnOpenKohya.BackColor = SystemColors.ButtonFace
        BtnOpenKohya.FlatStyle = FlatStyle.Flat
        BtnOpenKohya.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnOpenKohya.Location = New Point(369, 499)
        BtnOpenKohya.Name = "BtnOpenKohya"
        BtnOpenKohya.Size = New Size(178, 36)
        BtnOpenKohya.TabIndex = 17
        BtnOpenKohya.Text = "Open Kohya_ss"
        BtnOpenKohya.UseVisualStyleBackColor = False
        ' 
        ' BtnOpenTagger
        ' 
        BtnOpenTagger.BackColor = SystemColors.ButtonFace
        BtnOpenTagger.FlatStyle = FlatStyle.Flat
        BtnOpenTagger.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnOpenTagger.Location = New Point(369, 553)
        BtnOpenTagger.Name = "BtnOpenTagger"
        BtnOpenTagger.Size = New Size(178, 36)
        BtnOpenTagger.TabIndex = 18
        BtnOpenTagger.Text = "Open Tagger"
        BtnOpenTagger.UseVisualStyleBackColor = False
        ' 
        ' BtnCalcSteps
        ' 
        BtnCalcSteps.BackColor = SystemColors.ButtonFace
        BtnCalcSteps.FlatStyle = FlatStyle.Flat
        BtnCalcSteps.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnCalcSteps.Location = New Point(369, 605)
        BtnCalcSteps.Name = "BtnCalcSteps"
        BtnCalcSteps.Size = New Size(178, 36)
        BtnCalcSteps.TabIndex = 19
        BtnCalcSteps.Text = "Calculate Steps"
        BtnCalcSteps.UseVisualStyleBackColor = False
        ' 
        ' BtnChangeRootFolder
        ' 
        BtnChangeRootFolder.BackColor = Color.Red
        BtnChangeRootFolder.FlatStyle = FlatStyle.Popup
        BtnChangeRootFolder.Font = New Font("Cascadia Code", 9F, FontStyle.Regular, GraphicsUnit.Point)
        BtnChangeRootFolder.ForeColor = SystemColors.ButtonHighlight
        BtnChangeRootFolder.Location = New Point(405, 682)
        BtnChangeRootFolder.Name = "BtnChangeRootFolder"
        BtnChangeRootFolder.Size = New Size(97, 26)
        BtnChangeRootFolder.TabIndex = 20
        BtnChangeRootFolder.Text = "Change Root"
        BtnChangeRootFolder.UseVisualStyleBackColor = False
        ' 
        ' BtnChangeKohyaFolder
        ' 
        BtnChangeKohyaFolder.BackColor = Color.Brown
        BtnChangeKohyaFolder.FlatStyle = FlatStyle.Flat
        BtnChangeKohyaFolder.Location = New Point(560, 507)
        BtnChangeKohyaFolder.Name = "BtnChangeKohyaFolder"
        BtnChangeKohyaFolder.Size = New Size(23, 22)
        BtnChangeKohyaFolder.TabIndex = 21
        BtnChangeKohyaFolder.Text = "🔄️"
        BtnChangeKohyaFolder.UseVisualStyleBackColor = False
        ' 
        ' BtnChangeTaggerFolder
        ' 
        BtnChangeTaggerFolder.BackColor = Color.Brown
        BtnChangeTaggerFolder.FlatStyle = FlatStyle.Flat
        BtnChangeTaggerFolder.Location = New Point(560, 561)
        BtnChangeTaggerFolder.Name = "BtnChangeTaggerFolder"
        BtnChangeTaggerFolder.Size = New Size(23, 22)
        BtnChangeTaggerFolder.TabIndex = 22
        BtnChangeTaggerFolder.Text = "🔄️"
        BtnChangeTaggerFolder.UseVisualStyleBackColor = False
        ' 
        ' BtnUpdate
        ' 
        BtnUpdate.BackColor = Color.DeepSkyBlue
        BtnUpdate.FlatStyle = FlatStyle.Flat
        BtnUpdate.Font = New Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point)
        BtnUpdate.Location = New Point(332, 10)
        BtnUpdate.Name = "BtnUpdate"
        BtnUpdate.Size = New Size(93, 30)
        BtnUpdate.TabIndex = 23
        BtnUpdate.Text = "Refresh"
        BtnUpdate.UseVisualStyleBackColor = False
        ' 
        ' underline
        ' 
        underline.AutoSize = True
        underline.BackColor = Color.Transparent
        underline.Font = New Font("Segoe UI", 14.25F, FontStyle.Italic, GraphicsUnit.Point)
        underline.ForeColor = SystemColors.ButtonFace
        underline.Location = New Point(329, 61)
        underline.Name = "underline"
        underline.Size = New Size(188, 25)
        underline.TabIndex = 24
        underline.Text = "______________________"
        underline.TextAlign = ContentAlignment.MiddleCenter
        underline.Visible = False
        ' 
        ' LabelSelectedFolder
        ' 
        LabelSelectedFolder.AutoSize = True
        LabelSelectedFolder.BackColor = Color.Transparent
        LabelSelectedFolder.Font = New Font("Segoe UI", 14.25F, FontStyle.Italic, GraphicsUnit.Point)
        LabelSelectedFolder.ForeColor = SystemColors.ButtonFace
        LabelSelectedFolder.Location = New Point(330, 57)
        LabelSelectedFolder.Name = "LabelSelectedFolder"
        LabelSelectedFolder.Size = New Size(117, 25)
        LabelSelectedFolder.TabIndex = 25
        LabelSelectedFolder.Text = "PonyRealism"
        LabelSelectedFolder.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Black
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(597, 721)
        Controls.Add(LabelSelectedFolder)
        Controls.Add(underline)
        Controls.Add(BtnUpdate)
        Controls.Add(BtnChangeTaggerFolder)
        Controls.Add(BtnChangeKohyaFolder)
        Controls.Add(BtnChangeRootFolder)
        Controls.Add(BtnCalcSteps)
        Controls.Add(BtnOpenTagger)
        Controls.Add(BtnOpenKohya)
        Controls.Add(BtnCopyPath)
        Controls.Add(BtnOpenFolder)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(LabelHasModel)
        Controls.Add(Label3)
        Controls.Add(LabelHasCaptions)
        Controls.Add(Label2)
        Controls.Add(LabelImgCount)
        Controls.Add(Label1)
        Controls.Add(BtnDelete)
        Controls.Add(BtnNew)
        Controls.Add(BtnRetrain)
        Controls.Add(BtnDone)
        Controls.Add(BtnPending)
        Controls.Add(TreeView1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ZyloO's LoRA Manager"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents BtnPending As Button
    Friend WithEvents BtnDone As Button
    Friend WithEvents BtnRetrain As Button
    Friend WithEvents BtnNew As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents LabelImgCount As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelHasCaptions As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents LabelHasModel As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents BtnOpenFolder As Button
    Friend WithEvents BtnCopyPath As Button
    Friend WithEvents BtnOpenKohya As Button
    Friend WithEvents BtnOpenTagger As Button
    Friend WithEvents BtnCalcSteps As Button
    Friend WithEvents BtnChangeRootFolder As Button
    Friend WithEvents BtnChangeKohyaFolder As Button
    Friend WithEvents BtnChangeTaggerFolder As Button
    Friend WithEvents BtnUpdate As Button
    Friend WithEvents underline As Label
    Friend WithEvents LabelSelectedFolder As Label
End Class
