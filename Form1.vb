Imports System.IO
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Form1
    Private configFilePath As String
    Private config As JObject

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Get the path to the configuration file in %AppData%
        configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LoraTrainManager", "config.json")

        'Check if the configuration file exists
        If Not File.Exists(configFilePath) Then
            'Configuration file does not exist, prompt user for the main Lora folder
            Dim folderPath As String = GetLoraFolderPath()
            If Not String.IsNullOrEmpty(folderPath) Then
                'Create the configuration directory if it does not exist
                Dim configDirectory As String = Path.GetDirectoryName(configFilePath)
                If Not Directory.Exists(configDirectory) Then
                    Directory.CreateDirectory(configDirectory)
                End If

                'Construct the folder tree
                Dim folderTree As JToken = ConstructFolderTree(folderPath)

                'Save the path and tree to the configuration file as JSON
                config = New JObject(
                New JProperty("trainrootpath", folderPath),
                New JProperty("tree", folderTree)
            )

                Dim json As String = config.ToString(Formatting.Indented)
                File.WriteAllText(configFilePath, json)

                MessageBox.Show("Configuration file has been created.")
                PopulateTreeView(folderTree)
            Else
                MessageBox.Show("No folder was selected. The application will now exit.")
                Me.Close()
            End If
        Else
            'Configuration file exists, load the path and populate the TreeView
            Dim json As String = File.ReadAllText(configFilePath)
            config = JObject.Parse(json)

            'Check and update the folder tree
            CheckAndUpdateFolderTree()

            Dim folderTree As JToken = config("tree")
            PopulateTreeView(folderTree)
        End If

    End Sub

    Private Function GetLoraFolderPath() As String
        Using folderDialog As New FolderBrowserDialog()
            folderDialog.Description = "Select your main Lora folder"
            If folderDialog.ShowDialog() = DialogResult.OK Then
                Return folderDialog.SelectedPath
            Else
                Return String.Empty
            End If
        End Using
    End Function

    Private Sub PopulateTreeView(folderTree As JToken)
        'Save the expanded state of the tree nodes
        Dim expandedNodes As New List(Of String)
        SaveExpandedNodes(TreeView1.Nodes, expandedNodes)

        'Clear existing nodes
        TreeView1.Nodes.Clear()

        'Recursively add nodes from the folder tree JSON
        AddTreeNodes(folderTree, Nothing)

        'Restore the expanded state of the tree nodes
        RestoreExpandedNodes(TreeView1.Nodes, expandedNodes)

        'Add event handler for AfterSelect event
        AddHandler TreeView1.AfterSelect, AddressOf TreeView1_AfterSelect
    End Sub

    Private Sub AddTreeNodes(folderTree As JToken, parentNode As TreeNode)
        For Each folder As JToken In folderTree("children")
            Dim nodeName As String = folder("name").ToString()
            Dim nodePath As String = folder("path").ToString()
            Dim nodeState As String = If(folder("state") IsNot Nothing, folder("state").ToString(), "")

            Dim directoryNode As New TreeNode(nodeName)
            directoryNode.Tag = folder 'Store the JSON object in the Tag property for later use

            'Set background color based on state
            If nodeState = "pending" Then
                directoryNode.BackColor = Color.PaleVioletRed
            ElseIf nodeState = "done" Then
                directoryNode.BackColor = Color.LightSeaGreen
            ElseIf nodeState = "retrain" Then
                directoryNode.BackColor = Color.Orange
            End If

            If parentNode Is Nothing Then
                TreeView1.Nodes.Add(directoryNode)
            Else
                parentNode.Nodes.Add(directoryNode)
            End If

            'Recursively add child nodes
            AddTreeNodes(folder, directoryNode)
        Next
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs)
        'Get the selected node's JSON object from the Tag property
        Dim selectedFolder As JToken = CType(e.Node.Tag, JToken)

        'Debugging output to ensure the correct path is selected
        System.Diagnostics.Debug.WriteLine("Selected Path: " & selectedFolder("path").ToString())

        'Check if the selected path contains any of the filtered folders
        If Directory.Exists(Path.Combine(selectedFolder("path").ToString(), "img")) OrElse
           Directory.Exists(Path.Combine(selectedFolder("path").ToString(), "log")) OrElse
           Directory.Exists(Path.Combine(selectedFolder("path").ToString(), "model")) Then
            'Display Selected Folder Name
            LabelSelectedFolder.Text = Path.GetFileName(selectedFolder("path").ToString().TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar))
            underline.Visible = True
            BtnPending.Visible = True
            BtnDone.Visible = True
            BtnRetrain.Visible = True
            Label1.Visible = True
            Label2.Visible = True
            Label3.Visible = True
            Label3.Visible = True
            Label4.Visible = True
            Label5.Visible = True
            LabelImgCount.Visible = True
            LabelHasCaptions.Visible = True
            LabelHasModel.Visible = True
            BtnOpenFolder.Visible = True
            BtnCopyPath.Visible = True
            BtnCalcSteps.Visible = True
        Else
            'Clear Selected Folder Name
            LabelSelectedFolder.Text = ""
            underline.Visible = False
            BtnPending.Visible = False
            BtnDone.Visible = False
            BtnRetrain.Visible = False
            Label1.Visible = False
            Label2.Visible = False
            Label3.Visible = False
            Label3.Visible = False
            Label4.Visible = False
            Label5.Visible = False
            LabelImgCount.Visible = False
            LabelHasCaptions.Visible = False
            LabelHasModel.Visible = False
            BtnOpenFolder.Visible = False
            BtnCopyPath.Visible = False
            BtnCalcSteps.Visible = False
        End If

        'Calculate and display the file count (excluding .txt files)
        Dim imgFolderPath As String = Path.Combine(selectedFolder("path").ToString(), "img")
        If Directory.Exists(imgFolderPath) Then
            Dim fileCount As Integer = GetFileCount(imgFolderPath)
            LabelImgCount.Text = fileCount.ToString()

            'Check if there are any .txt files in the img folder
            Dim hasTxt As Boolean = HasTxtFiles(imgFolderPath)
            LabelHasCaptions.Text = If(hasTxt, "Yes", "No")
        Else
            LabelImgCount.Text = "0"
            LabelHasCaptions.Text = "No"
        End If

        'Check if there are any .safetensors files in the model folder
        Dim modelFolderPath As String = Path.Combine(selectedFolder("path").ToString(), "model")
        If Directory.Exists(modelFolderPath) Then
            Dim hasSafetensors As Boolean = HasSafetensorsFiles(modelFolderPath)
            LabelHasModel.Text = If(hasSafetensors, "Yes", "No")
        Else
            LabelHasModel.Text = "No"
        End If
    End Sub

    Private Sub BtnPending_Click(sender As Object, e As EventArgs) Handles BtnPending.Click
        'Get the selected node
        If TreeView1.SelectedNode IsNot Nothing Then
            Dim selectedFolder As JToken = CType(TreeView1.SelectedNode.Tag, JToken)
            selectedFolder("state") = "pending"

            'Save the updated JSON back to the file
            File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))

            'Repopulate the TreeView
            PopulateTreeView(config("tree"))
        Else
            MessageBox.Show("Please select a folder.")
        End If
    End Sub
    Private Sub BtnDone_Click(sender As Object, e As EventArgs) Handles BtnDone.Click
        'Get the selected node
        If TreeView1.SelectedNode IsNot Nothing Then
            Dim selectedFolder As JToken = CType(TreeView1.SelectedNode.Tag, JToken)
            selectedFolder("state") = "done"

            'Save the updated JSON back to the file
            File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))

            'Repopulate the TreeView
            PopulateTreeView(config("tree"))
        Else
            MessageBox.Show("Please select a folder.")
        End If
    End Sub
    Private Sub BtnRetrain_Click(sender As Object, e As EventArgs) Handles BtnRetrain.Click
        'Get the selected node
        If TreeView1.SelectedNode IsNot Nothing Then
            Dim selectedFolder As JToken = CType(TreeView1.SelectedNode.Tag, JToken)
            selectedFolder("state") = "retrain"

            'Save the updated JSON back to the file
            File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))

            'Repopulate the TreeView
            PopulateTreeView(config("tree"))
        Else
            MessageBox.Show("Please select a folder.")
        End If
    End Sub

    Private Function ConstructFolderTree(rootPath As String) As JToken
        Dim rootDirectory As New DirectoryInfo(rootPath)
        Dim rootNode As New JObject(New JProperty("name", rootDirectory.Name), New JProperty("path", rootDirectory.FullName))

        'Add child directories recursively
        Dim childNodes As New JArray()
        AddChildDirectories(rootDirectory, childNodes)
        rootNode.Add(New JProperty("children", childNodes))

        Return rootNode
    End Function

    Private Sub AddChildDirectories(directoryInfo As DirectoryInfo, parentArray As JArray)
        For Each d As DirectoryInfo In directoryInfo.GetDirectories()
            'Skip excluded directories
            If d.Name.ToLower() = "img" OrElse d.Name.ToLower() = "log" OrElse d.Name.ToLower() = "model" Then
                Continue For
            End If

            Dim childNode As New JObject(New JProperty("name", d.Name), New JProperty("path", d.FullName))

            'Check if the directory contains the filtered folders
            If Directory.Exists(Path.Combine(d.FullName, "img")) OrElse
               Directory.Exists(Path.Combine(d.FullName, "log")) OrElse
               Directory.Exists(Path.Combine(d.FullName, "model")) Then
                childNode.Add(New JProperty("state", "pending"))
            End If

            'Add child directories recursively
            Dim childArray As New JArray()
            AddChildDirectories(d, childArray)
            childNode.Add(New JProperty("children", childArray))

            parentArray.Add(childNode)
        Next
    End Sub

    Private Function GetFileCount(folderPath As String) As Integer
        Dim fileCount As Integer = 0
        Try
            'Get all files in the directory and subdirectories, excluding .txt files
            Dim files As String() = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
            fileCount = files.Count(Function(f) Path.GetExtension(f).ToLower() <> ".txt")
        Catch ex As Exception
            MessageBox.Show("Error counting files: " & ex.Message)
        End Try
        Return fileCount
    End Function

    Private Function HasTxtFiles(folderPath As String) As Boolean
        Try
            'Get all .txt files in the directory and subdirectories
            Dim txtFiles As String() = Directory.GetFiles(folderPath, "*.txt", SearchOption.AllDirectories)
            Return txtFiles.Length > 0
        Catch ex As Exception
            MessageBox.Show("Error checking for .txt files: " & ex.Message)
        End Try
        Return False
    End Function

    Private Function HasSafetensorsFiles(folderPath As String) As Boolean
        Try
            'Get all .safetensors files in the directory and subdirectories
            Dim safetensorsFiles As String() = Directory.GetFiles(folderPath, "*.safetensors", SearchOption.AllDirectories)
            Return safetensorsFiles.Length > 0
        Catch ex As Exception
            MessageBox.Show("Error checking for .safetensors files: " & ex.Message)
        End Try
        Return False
    End Function

    Private Sub SaveExpandedNodes(nodes As TreeNodeCollection, expandedNodes As List(Of String))
        For Each node As TreeNode In nodes
            If node.IsExpanded Then
                expandedNodes.Add(node.FullPath)
            End If
            SaveExpandedNodes(node.Nodes, expandedNodes)
        Next
    End Sub

    Private Sub RestoreExpandedNodes(nodes As TreeNodeCollection, expandedNodes As List(Of String))
        For Each node As TreeNode In nodes
            If expandedNodes.Contains(node.FullPath) Then
                node.Expand()
            End If
            RestoreExpandedNodes(node.Nodes, expandedNodes)
        Next
    End Sub
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        'Get the selected node
        If TreeView1.SelectedNode IsNot Nothing Then
            Dim selectedFolder As JToken = CType(TreeView1.SelectedNode.Tag, JToken)
            Dim folderPath As String = selectedFolder("path").ToString()

            'Confirm deletion
            If MessageBox.Show($"Are you sure you want to delete the folder: {folderPath}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    'Delete the folder
                    Directory.Delete(folderPath, True)

                    'Remove the folder from the JSON structure
                    RemoveFolderFromJson(config("tree"), folderPath)

                    'Save the updated JSON back to the file
                    File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))

                    'Repopulate the TreeView
                    PopulateTreeView(config("tree"))

                    MessageBox.Show("Folder deleted successfully.")
                Catch ex As Exception
                    MessageBox.Show($"Error deleting folder: {ex.Message}")
                End Try
            End If
        Else
            MessageBox.Show("Please select a folder.")
        End If
    End Sub

    Private Sub RemoveFolderFromJson(folderTree As JToken, folderPath As String)
        Dim children As JArray = CType(folderTree("children"), JArray)
        For Each folder As JToken In children.ToList()
            If folder("path").ToString() = folderPath Then
                children.Remove(folder)
                Exit For
            Else
                RemoveFolderFromJson(folder, folderPath)
            End If
        Next
    End Sub

    Private Sub BtnOpenFolder_Click(sender As Object, e As EventArgs) Handles BtnOpenFolder.Click
        'Get the selected node
        If TreeView1.SelectedNode IsNot Nothing Then
            Dim selectedFolder As JToken = CType(TreeView1.SelectedNode.Tag, JToken)
            Dim folderPath As String = selectedFolder("path").ToString()

            Try
                'Open the selected folder in Windows Explorer
                Process.Start("explorer.exe", folderPath)
            Catch ex As Exception
                MessageBox.Show($"Error opening folder: {ex.Message}")
            End Try
        Else
            MessageBox.Show("Please select a folder.")
        End If
    End Sub

    Private Sub BtnCopyPath_Click(sender As Object, e As EventArgs) Handles BtnCopyPath.Click
        'Get the selected node
        If TreeView1.SelectedNode IsNot Nothing Then
            Dim selectedFolder As JToken = CType(TreeView1.SelectedNode.Tag, JToken)
            Dim folderPath As String = selectedFolder("path").ToString()

            Try
                'Copy the selected folder path to the clipboard
                Clipboard.SetText(folderPath)
                MessageBox.Show("Folder path copied to clipboard.")
            Catch ex As Exception
                MessageBox.Show($"Error copying folder path: {ex.Message}")
            End Try
        Else
            MessageBox.Show("Please select a folder.")
        End If
    End Sub

    Private Sub BtnOpenKohya_Click(sender As Object, e As EventArgs) Handles BtnOpenKohya.Click
        'Get the kohya path from the config.json
        Dim kohyaPath As String = If(config("kohya") IsNot Nothing, config("kohya").ToString(), String.Empty)

        'If kohya path is not found in the config.json, prompt the user to select it
        If String.IsNullOrEmpty(kohyaPath) Then
            Using folderDialog As New FolderBrowserDialog()
                folderDialog.Description = "Select the Kohya directory"
                If folderDialog.ShowDialog() = DialogResult.OK Then
                    kohyaPath = folderDialog.SelectedPath

                    'Save the kohya path to the config.json
                    config("kohya") = kohyaPath
                    File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))
                Else
                    MessageBox.Show("No folder was selected. Operation cancelled.")
                    Return
                End If
            End Using
        End If

        'Construct the path to gui.bat
        Dim guiBatPath As String = Path.Combine(kohyaPath, "gui.bat")

        'Check if gui.bat exists and launch it
        If File.Exists(guiBatPath) Then
            Try
                Process.Start(New ProcessStartInfo() With {
                    .FileName = guiBatPath,
                    .WorkingDirectory = kohyaPath,
                    .UseShellExecute = True
                })
            Catch ex As Exception
                MessageBox.Show($"Error launching gui.bat: {ex.Message}")
            End Try
        Else
            MessageBox.Show("gui.bat not found in the specified Kohya directory.")
        End If
    End Sub

    Private Sub BtnOpenTagger_Click(sender As Object, e As EventArgs) Handles BtnOpenTagger.Click
        'Get the tagger path from the config.json
        Dim taggerPath As String = If(config("tagger") IsNot Nothing, config("tagger").ToString(), String.Empty)

        'If tagger path is not found in the config.json, prompt the user to select it
        If String.IsNullOrEmpty(taggerPath) Then
            Using folderDialog As New FolderBrowserDialog()
                folderDialog.Description = "Select the Tagger directory"
                If folderDialog.ShowDialog() = DialogResult.OK Then
                    taggerPath = folderDialog.SelectedPath

                    'Save the tagger path to the config.json
                    config("tagger") = taggerPath
                    File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))
                Else
                    MessageBox.Show("No folder was selected. Operation cancelled.")
                    Return
                End If
            End Using
        End If

        'Construct the path to BooruDatasetTagManager.exe
        Dim exePath As String = Path.Combine(taggerPath, "BooruDatasetTagManager.exe")

        'Check if BooruDatasetTagManager.exe exists and launch it
        If File.Exists(exePath) Then
            Try
                Process.Start(New ProcessStartInfo() With {
                    .FileName = exePath,
                    .WorkingDirectory = taggerPath,
                    .UseShellExecute = True
                })
            Catch ex As Exception
                MessageBox.Show($"Error launching BooruDatasetTagManager.exe: {ex.Message}")
            End Try
        Else
            MessageBox.Show("BooruDatasetTagManager.exe not found in the specified Tagger directory.")
        End If
    End Sub

    Private Sub BtnCalcSteps_Click(sender As Object, e As EventArgs) Handles BtnCalcSteps.Click
        'Get the selected node
        If TreeView1.SelectedNode IsNot Nothing Then
            Dim selectedFolder As JToken = CType(TreeView1.SelectedNode.Tag, JToken)
            Dim imgFolderPath As String = Path.Combine(selectedFolder("path").ToString(), "img")

            If Directory.Exists(imgFolderPath) Then
                'Calculate the number of images (excluding .txt files)
                Dim numImages As Integer = GetFileCount(imgFolderPath)
                Dim batchSizeInput As String = InputBox("Enter the batch size:", "Batch Size")
                Dim batchSize As Integer

                'Validate the batch size input
                If Integer.TryParse(batchSizeInput, batchSize) AndAlso batchSize > 0 Then
                    'Perform the logic to calculate repeats, epochs, and total steps
                    Dim factor1 As Integer
                    Dim result As String = CalculateSteps(numImages, batchSize, factor1)
                    If MessageBox.Show(result, "Calculation Results") = DialogResult.OK Then
                        'Ask the user if they want to update the image folder
                        If MessageBox.Show("Do you want to update the image folder name?", "Update Folder", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            'Open a new dialog for string input
                            Dim userInput As String = InputBox("Enter subject input for the folder name: (woman,man...)", "Rename Folder")

                            'Check if the user clicked cancel (userInput will be empty)
                            If Not String.IsNullOrEmpty(userInput) Then
                                'Rename the folder inside the "img" folder
                                RenameFolder(imgFolderPath, factor1, userInput)
                            End If
                        End If
                    End If
                Else
                    MessageBox.Show("Invalid batch size entered. Operation cancelled.")
                End If
            Else
                MessageBox.Show("The img folder does not exist in the selected directory.")
            End If
        Else
            MessageBox.Show("Please select a folder.")
        End If
    End Sub


    Private Function CalculateSteps(numImages As Integer, batchSize As Integer, ByRef factor1 As Integer) As String
        Dim min1 As Integer = 900
        Dim max1 As Integer = 1000
        Dim min2 As Integer = 3000
        Dim max2 As Integer = 4500

        Dim factor2 As Integer = 1
        Dim found1 As Boolean = False
        Dim found2 As Boolean = False
        Dim result1 As Integer = 0
        Dim result2 As Integer = 0
        Dim maxAttempts As Integer = 10 ' Maximum number of attempts to adjust the range
        Dim attempts As Integer = 0

        Do While attempts < maxAttempts AndAlso Not found1
            ' Find suitable factor1
            For i As Integer = 1 To 1000
                result1 = numImages * i
                If result1 >= min1 AndAlso result1 <= max1 Then
                    factor1 = i
                    found1 = True
                    Exit For
                End If
            Next

            If Not found1 Then
                ' Adjust the range and try again
                min1 -= 100
                max1 += 100
                attempts += 1
            End If
        Loop

        If found1 Then
            Dim dividedResult As Integer = result1 \ batchSize

            attempts = 0
            Do While attempts < maxAttempts AndAlso Not found2
                ' Find suitable factor2
                For j As Integer = 1 To 1000
                    result2 = dividedResult * j
                    If result2 >= min2 AndAlso result2 <= max2 Then
                        factor2 = j
                        found2 = True
                        Exit For
                    End If
                Next

                If Not found2 Then
                    ' Adjust the range and try again
                    min2 -= 500
                    max2 += 500
                    attempts += 1
                End If
            Loop

            If found2 Then
                Return $"Repeats = {factor1}." & vbCrLf &
                   $"Epochs = {factor2}." & vbCrLf &
                   $"Total steps = {result2}."
            Else
                Return "No suitable factor found to get a result between 3000 and 4000 after multiple attempts."
            End If
        Else
            Return "No suitable factor found to get a result between 900 and 1000 after multiple attempts."
        End If
    End Function
    Private Sub RenameFolder(imgFolderPath As String, factor1 As Integer, userInput As String)
        Try
            'Get the first folder inside the "img" folder
            Dim directories As String() = Directory.GetDirectories(imgFolderPath)
            If directories.Length > 0 Then
                Dim oldFolderPath As String = directories(0)
                Dim newFolderName As String = $"{factor1}_{userInput}"
                Dim newFolderPath As String = Path.Combine(imgFolderPath, newFolderName)

                'Rename the folder
                Directory.Move(oldFolderPath, newFolderPath)
                MessageBox.Show($"Folder renamed to: {newFolderName}")
            Else
                MessageBox.Show("No subfolders found inside the img folder.")
            End If
        Catch ex As Exception
            MessageBox.Show($"Error renaming folder: {ex.Message}")
        End Try
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        'Get the selected node
        If TreeView1.SelectedNode IsNot Nothing Then
            Dim selectedFolder As JToken = CType(TreeView1.SelectedNode.Tag, JToken)
            Dim parentFolderPath As String = selectedFolder("path").ToString()

            'Check if the parent folder contains "img", "log", and "model" folders
            If Directory.Exists(Path.Combine(parentFolderPath, "img")) OrElse
               Directory.Exists(Path.Combine(parentFolderPath, "log")) OrElse
               Directory.Exists(Path.Combine(parentFolderPath, "model")) Then
                MessageBox.Show("Cannot create a new folder. Please select a parent folder.")
                Return
            End If

            'Prompt for the new folder name
            Dim newFolderName As String = InputBox("Enter the name for the new folder:", "New Folder")
            If String.IsNullOrEmpty(newFolderName) Then
                MessageBox.Show("Folder name cannot be empty.")
                Return
            End If

            'Create the new folder with the specific structure
            Try
                Dim newFolderPath As String = Path.Combine(parentFolderPath, newFolderName)
                Directory.CreateDirectory(newFolderPath)
                Directory.CreateDirectory(Path.Combine(newFolderPath, "img"))
                Directory.CreateDirectory(Path.Combine(newFolderPath, "log"))
                Directory.CreateDirectory(Path.Combine(newFolderPath, "model"))

                'Create a placeholder subfolder inside the "img" folder
                Directory.CreateDirectory(Path.Combine(newFolderPath, "img", "repeats_subject"))

                'Update the JSON structure
                Dim newFolderJson As New JObject(
                    New JProperty("name", newFolderName),
                    New JProperty("path", newFolderPath),
                    New JProperty("state", "pending"),
                    New JProperty("children", New JArray())
                )
                CType(selectedFolder("children"), JArray).Add(newFolderJson)

                'Save the updated JSON back to the file
                File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))

                'Update the tree view
                PopulateTreeView(config("tree"))

                MessageBox.Show("New folder structure created and tree updated successfully.")
            Catch ex As Exception
                MessageBox.Show($"Error creating new folder: {ex.Message}")
            End Try
        Else
            MessageBox.Show("Please select a parent folder.")
        End If
    End Sub

    Private Sub BtnChangeKohyaFolder_Click(sender As Object, e As EventArgs) Handles BtnChangeKohyaFolder.Click
        'Open the folder selection dialog
        Using folderDialog As New FolderBrowserDialog()
            folderDialog.Description = "Select the Kohya directory"
            If folderDialog.ShowDialog() = DialogResult.OK Then
                Dim kohyaPath As String = folderDialog.SelectedPath

                'Update the kohya path in the config.json
                config("kohya") = kohyaPath
                File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))

                MessageBox.Show("Kohya directory path updated successfully.")
            Else
                MessageBox.Show("No folder was selected. Operation cancelled.")
            End If
        End Using
    End Sub

    Private Sub BtnChangeTaggerFolder_Click(sender As Object, e As EventArgs) Handles BtnChangeTaggerFolder.Click
        'Open the folder selection dialog
        Using folderDialog As New FolderBrowserDialog()
            folderDialog.Description = "Select the Tagger directory"
            If folderDialog.ShowDialog() = DialogResult.OK Then
                Dim taggerPath As String = folderDialog.SelectedPath

                'Update the tagger path in the config.json
                config("tagger") = taggerPath
                File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))

                MessageBox.Show("Tagger directory path updated successfully.")
            Else
                MessageBox.Show("No folder was selected. Operation cancelled.")
            End If
        End Using
    End Sub

    Private Sub BtnChangeRootFolder_Click(sender As Object, e As EventArgs) Handles BtnChangeRootFolder.Click
        'Confirm with the user before proceeding
        If MessageBox.Show("Are you sure you want to change the root folder? This will erase the current folder statuses.", "Confirm Change Root Folder", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            'Open the folder selection dialog
            Using folderDialog As New FolderBrowserDialog()
                folderDialog.Description = "Select the new root folder"
                If folderDialog.ShowDialog() = DialogResult.OK Then
                    Dim newRootFolderPath As String = folderDialog.SelectedPath
                    Dim oldTree As JToken = config("tree")

                    'Build the new folder structure
                    Dim newTree As JToken = ConstructFolderTree(newRootFolderPath)

                    'Migrate statuses from the old tree to the new one
                    MigrateStatuses(oldTree, newTree)

                    'Update the JSON structure with the new root path and new tree
                    config("trainrootpath") = newRootFolderPath
                    config("tree") = newTree

                    'Save the updated JSON back to the file
                    File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))

                    'Update the tree view
                    PopulateTreeView(config("tree"))

                    MessageBox.Show("Root folder changed and tree updated successfully.")
                Else
                    MessageBox.Show("No folder was selected. Operation cancelled.")
                End If
            End Using
        End If
    End Sub
    Private Sub MigrateStatuses(oldTree As JToken, newTree As JToken)
        Dim oldFolders As New Dictionary(Of String, String)
        CollectStatuses(oldTree, oldFolders)

        ApplyStatuses(newTree, oldFolders)
    End Sub

    Private Sub CollectStatuses(currentNode As JToken, folderStatuses As Dictionary(Of String, String))
        If currentNode Is Nothing Then Return

        For Each child As JToken In currentNode("children")
            Dim folderPath As String = child("path").ToString()
            If child("state") IsNot Nothing Then
                folderStatuses(folderPath) = child("state").ToString()
            End If
            CollectStatuses(child, folderStatuses)
        Next
    End Sub

    Private Sub ApplyStatuses(currentNode As JToken, folderStatuses As Dictionary(Of String, String))
        If currentNode Is Nothing Then Return

        For Each child As JToken In currentNode("children")
            Dim folderPath As String = child("path").ToString()
            If folderStatuses.ContainsKey(folderPath) Then
                child("state") = folderStatuses(folderPath)
            End If
            ApplyStatuses(child, folderStatuses)
        Next
    End Sub

    Private Sub CheckAndUpdateFolderTree()
        'Get the root path from the config
        Dim rootPath As String = config("trainrootpath").ToString()
        'Build the current folder structure
        Dim currentTree As JToken = ConstructFolderTree(rootPath)
        'Get the stored folder tree
        Dim storedTree As JToken = config("tree")
        'Update the stored folder tree with the current folder structure
        UpdateStoredTree(currentTree, storedTree)

        'Save the updated JSON back to the file
        File.WriteAllText(configFilePath, config.ToString(Formatting.Indented))
    End Sub

    Private Sub UpdateStoredTree(currentNode As JToken, storedNode As JToken)
        Dim storedChildren As JArray = CType(storedNode("children"), JArray)
        Dim currentChildren As JArray = CType(currentNode("children"), JArray)

        'Create a dictionary for quick lookup of stored children
        Dim storedChildrenDict As New Dictionary(Of String, JToken)
        For Each child As JToken In storedChildren
            storedChildrenDict(child("path").ToString()) = child
        Next

        'Update or add current children
        For Each currentChild As JToken In currentChildren
            Dim path As String = currentChild("path").ToString()
            If storedChildrenDict.ContainsKey(path) Then
                'Update existing child
                UpdateStoredTree(currentChild, storedChildrenDict(path))
            Else
                'Add new child and set its status to pending
                currentChild("state") = "pending"
                storedChildren.Add(currentChild)
            End If
        Next

        'Remove stored children that no longer exist
        For Each storedChild As JToken In storedChildren.ToList()
            Dim path As String = storedChild("path").ToString()
            If Not currentChildren.Any(Function(c) c("path").ToString() = path) Then
                storedChildren.Remove(storedChild)
            End If
        Next
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        CheckAndUpdateFolderTree()
    End Sub
End Class

