Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports Atalasoft.Imaging
Imports Atalasoft.Imaging.Codec
Imports Atalasoft.Imaging.ImageProcessing.Document
Imports WinDemoHelperMethods.WinDemoHelperMethods


Namespace ColorRegionDemo
    ''' <summary>
    ''' 	<para>This application demonstrates using the Atalasoft.dotImage.DocClean add-on
    '''     for Atalasoft Document Imaging to split an image into separate color and grayscale
    '''     sections, which can then be saved using different compressions for overall better
    '''     compression ratios.</para>
    ''' </summary>
    Public Class Form1
        Inherits System.Windows.Forms.Form
        Private _command As Atalasoft.Imaging.ImageProcessing.Document.ColorExtractionCommand
        Private mainMenu1 As System.Windows.Forms.MainMenu
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
        Private imageList1 As System.Windows.Forms.ImageList
        Private menuItem1 As System.Windows.Forms.MenuItem
        Private WithEvents menuOpen As System.Windows.Forms.MenuItem
        Private WithEvents menuExit As System.Windows.Forms.MenuItem
        Private menuCommand As System.Windows.Forms.MenuItem
        Private WithEvents menuCommandSettings As System.Windows.Forms.MenuItem
        Private menuItem5 As System.Windows.Forms.MenuItem
        Private WithEvents menuCommandProcess As System.Windows.Forms.MenuItem
        Private tbOpen As System.Windows.Forms.ToolBarButton
        Private tbSettings As System.Windows.Forms.ToolBarButton
        Private tbProcess As System.Windows.Forms.ToolBarButton
        Private tbAbout As System.Windows.Forms.ToolBarButton
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton
        Private toolBarButton2 As System.Windows.Forms.ToolBarButton
        Private menuHelp As System.Windows.Forms.MenuItem
        Private WithEvents menuHelpAbout As System.Windows.Forms.MenuItem
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private label1 As System.Windows.Forms.Label
        Private lblColorLabel As System.Windows.Forms.Label
        Private panelOriginal As System.Windows.Forms.Panel
        Private WithEvents _originalViewer As Atalasoft.Imaging.WinControls.ImageViewer
        Private panelColor As System.Windows.Forms.Panel
        Private WithEvents _colorViewer As Atalasoft.Imaging.WinControls.ImageViewer
        Private sbpImageFile As System.Windows.Forms.StatusBarPanel
        Private sbpProcessingTime As System.Windows.Forms.StatusBarPanel
        Private sbpPixelColor As System.Windows.Forms.StatusBarPanel
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            HelperMethods.PopulateDecoders(RegisteredDecoders.Decoders)

            ' This demo requires a Document Imaging license.
            If Atalasoft.Imaging.AtalaImage.Edition <> Atalasoft.Imaging.LicenseEdition.Document Then
                MessageBox.Show(Me, "This demo requires an Atalasoft DotImage Document Imaging license." & Constants.vbCrLf & "Your current license is " & Atalasoft.Imaging.AtalaImage.Edition.ToString() & ".", "Incorrect License", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End If
            If Atalasoft.Licensing.AtalaLicenseProvider.GetLicenseFlag("Atalasoft.dotImage", "AdvancedDocClean") Is Nothing Then
                MessageBox.Show(Me, "This demo requires an Atalasoft DotImage Advanced Document Cleanup license and will close.", "Incorrect License", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End If

            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            Me._command = New Atalasoft.Imaging.ImageProcessing.Document.ColorExtractionCommand()
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
            Me.mainMenu1 = New System.Windows.Forms.MainMenu()
            Me.menuItem1 = New System.Windows.Forms.MenuItem()
            Me.menuOpen = New System.Windows.Forms.MenuItem()
            Me.menuExit = New System.Windows.Forms.MenuItem()
            Me.menuCommand = New System.Windows.Forms.MenuItem()
            Me.menuCommandSettings = New System.Windows.Forms.MenuItem()
            Me.menuItem5 = New System.Windows.Forms.MenuItem()
            Me.menuCommandProcess = New System.Windows.Forms.MenuItem()
            Me.menuHelp = New System.Windows.Forms.MenuItem()
            Me.menuHelpAbout = New System.Windows.Forms.MenuItem()
            Me.toolBar1 = New System.Windows.Forms.ToolBar()
            Me.tbOpen = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton()
            Me.tbSettings = New System.Windows.Forms.ToolBarButton()
            Me.tbProcess = New System.Windows.Forms.ToolBarButton()
            Me.toolBarButton2 = New System.Windows.Forms.ToolBarButton()
            Me.tbAbout = New System.Windows.Forms.ToolBarButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.sbpImageFile = New System.Windows.Forms.StatusBarPanel()
            Me.sbpProcessingTime = New System.Windows.Forms.StatusBarPanel()
            Me.sbpPixelColor = New System.Windows.Forms.StatusBarPanel()
            Me.label1 = New System.Windows.Forms.Label()
            Me.lblColorLabel = New System.Windows.Forms.Label()
            Me.panelOriginal = New System.Windows.Forms.Panel()
            Me._originalViewer = New Atalasoft.Imaging.WinControls.ImageViewer()
            Me.panelColor = New System.Windows.Forms.Panel()
            Me._colorViewer = New Atalasoft.Imaging.WinControls.ImageViewer()
            CType(Me.sbpImageFile, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.sbpProcessingTime, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.sbpPixelColor, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panelOriginal.SuspendLayout()
            Me.panelColor.SuspendLayout()
            Me.SuspendLayout()
            ' 
            ' mainMenu1
            ' 
            Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuItem1, Me.menuCommand, Me.menuHelp})
            ' 
            ' menuItem1
            ' 
            Me.menuItem1.Index = 0
            Me.menuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuOpen, Me.menuExit})
            Me.menuItem1.Text = "&File"
            ' 
            ' menuOpen
            ' 
            Me.menuOpen.Index = 0
            Me.menuOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
            Me.menuOpen.Text = "&Open"
            '			Me.menuOpen.Click += New System.EventHandler(Me.menuOpen_Click);
            ' 
            ' menuExit
            ' 
            Me.menuExit.Index = 1
            Me.menuExit.Text = "E&xit"
            '			Me.menuExit.Click += New System.EventHandler(Me.menuExit_Click);
            ' 
            ' menuCommand
            ' 
            Me.menuCommand.Index = 1
            Me.menuCommand.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuCommandSettings, Me.menuItem5, Me.menuCommandProcess})
            Me.menuCommand.Text = "&Command"
            ' 
            ' menuCommandSettings
            ' 
            Me.menuCommandSettings.Index = 0
            Me.menuCommandSettings.Text = "&Settings..."
            '			Me.menuCommandSettings.Click += New System.EventHandler(Me.menuCommandSettings_Click);
            ' 
            ' menuItem5
            ' 
            Me.menuItem5.Index = 1
            Me.menuItem5.Text = "-"
            ' 
            ' menuCommandProcess
            ' 
            Me.menuCommandProcess.Index = 2
            Me.menuCommandProcess.Text = "&Process Image"
            '			Me.menuCommandProcess.Click += New System.EventHandler(Me.menuCommandProcess_Click);
            ' 
            ' menuHelp
            ' 
            Me.menuHelp.Index = 2
            Me.menuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuHelpAbout})
            Me.menuHelp.Text = "&Help"
            ' 
            ' menuHelpAbout
            ' 
            Me.menuHelpAbout.Index = 0
            Me.menuHelpAbout.Text = "&About..."
            '			Me.menuHelpAbout.Click += New System.EventHandler(Me.menuHelpAbout_Click);
            ' 
            ' toolBar1
            ' 
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbOpen, Me.toolBarButton1, Me.tbSettings, Me.tbProcess, Me.toolBarButton2, Me.tbAbout})
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(920, 36)
            Me.toolBar1.TabIndex = 0
            '			Me.toolBar1.ButtonClick += New System.Windows.Forms.ToolBarButtonClickEventHandler(Me.toolBar1_ButtonClick);
            ' 
            ' tbOpen
            ' 
            Me.tbOpen.ImageIndex = 4
            Me.tbOpen.ToolTipText = "Open"
            ' 
            ' toolBarButton1
            ' 
            Me.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            ' 
            ' tbSettings
            ' 
            Me.tbSettings.ImageIndex = 2
            Me.tbSettings.ToolTipText = "Command Settings"
            ' 
            ' tbProcess
            ' 
            Me.tbProcess.Enabled = False
            Me.tbProcess.ImageIndex = 1
            Me.tbProcess.ToolTipText = "Process Command"
            ' 
            ' toolBarButton2
            ' 
            Me.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            ' 
            ' tbAbout
            ' 
            Me.tbAbout.ImageIndex = 5
            Me.tbAbout.ToolTipText = "About..."
            ' 
            ' imageList1
            ' 
            Me.imageList1.ImageSize = New System.Drawing.Size(24, 24)
            Me.imageList1.ImageStream = (CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer))
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            ' 
            ' statusBar1
            ' 
            Me.statusBar1.Location = New System.Drawing.Point(0, 595)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbpImageFile, Me.sbpProcessingTime, Me.sbpPixelColor})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(920, 22)
            Me.statusBar1.TabIndex = 1
            ' 
            ' sbpImageFile
            ' 
            Me.sbpImageFile.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
            Me.sbpImageFile.Text = "No Image Loaded"
            Me.sbpImageFile.Width = 104
            ' 
            ' sbpProcessingTime
            ' 
            Me.sbpProcessingTime.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
            Me.sbpProcessingTime.Width = 10
            ' 
            ' sbpPixelColor
            ' 
            Me.sbpPixelColor.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.sbpPixelColor.Width = 790
            ' 
            ' label1
            ' 
            Me.label1.AutoSize = True
            Me.label1.Location = New System.Drawing.Point(0, 40)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(78, 16)
            Me.label1.TabIndex = 4
            Me.label1.Text = "Original Image"
            ' 
            ' lblColorLabel
            ' 
            Me.lblColorLabel.AutoSize = True
            Me.lblColorLabel.Location = New System.Drawing.Point(456, 40)
            Me.lblColorLabel.Name = "lblColorLabel"
            Me.lblColorLabel.Size = New System.Drawing.Size(117, 16)
            Me.lblColorLabel.TabIndex = 5
            Me.lblColorLabel.Text = "Extracted Color Image"
            ' 
            ' panelOriginal
            ' 
            Me.panelOriginal.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
            Me.panelOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.panelOriginal.Controls.Add(Me._originalViewer)
            Me.panelOriginal.Location = New System.Drawing.Point(0, 56)
            Me.panelOriginal.Name = "panelOriginal"
            Me.panelOriginal.Size = New System.Drawing.Size(448, 528)
            Me.panelOriginal.TabIndex = 6
            ' 
            ' _originalViewer
            ' 
            Me._originalViewer.AntialiasDisplay = Atalasoft.Imaging.WinControls.AntialiasDisplayMode.ScaleToGray
            Me._originalViewer.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.BestFitShrinkOnly
            Me._originalViewer.Centered = True
            Me._originalViewer.DisplayProfile = Nothing
            Me._originalViewer.Dock = System.Windows.Forms.DockStyle.Fill
            Me._originalViewer.Location = New System.Drawing.Point(0, 0)
            Me._originalViewer.Magnifier.BackColor = System.Drawing.Color.White
            Me._originalViewer.Magnifier.BorderColor = System.Drawing.Color.Black
            Me._originalViewer.Magnifier.Size = New System.Drawing.Size(100, 100)
            Me._originalViewer.Name = "_originalViewer"
            Me._originalViewer.OutputProfile = Nothing
            Me._originalViewer.Selection = Nothing
            Me._originalViewer.Size = New System.Drawing.Size(446, 526)
            Me._originalViewer.TabIndex = 3
            Me._originalViewer.Text = "_originalViewer"
            '			Me._originalViewer.MouseMovePixel += New System.Windows.Forms.MouseEventHandler(Me._originalViewer_MouseMovePixel);
            Me._originalViewer.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.Magnifier
            ' 
            ' panelColor
            ' 
            Me.panelColor.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
            Me.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.panelColor.Controls.Add(Me._colorViewer)
            Me.panelColor.Location = New System.Drawing.Point(456, 56)
            Me.panelColor.Name = "panelColor"
            Me.panelColor.Size = New System.Drawing.Size(456, 528)
            Me.panelColor.TabIndex = 7
            ' 
            ' _colorViewer
            ' 
            Me._colorViewer.AntialiasDisplay = Atalasoft.Imaging.WinControls.AntialiasDisplayMode.ScaleToGray
            Me._colorViewer.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.BestFitShrinkOnly
            Me._colorViewer.Centered = True
            Me._colorViewer.DisplayProfile = Nothing
            Me._colorViewer.Dock = System.Windows.Forms.DockStyle.Fill
            Me._colorViewer.Location = New System.Drawing.Point(0, 0)
            Me._colorViewer.Magnifier.BackColor = System.Drawing.Color.White
            Me._colorViewer.Magnifier.BorderColor = System.Drawing.Color.Black
            Me._colorViewer.Magnifier.Size = New System.Drawing.Size(100, 100)
            Me._colorViewer.Name = "_colorViewer"
            Me._colorViewer.OutputProfile = Nothing
            Me._colorViewer.Selection = Nothing
            Me._colorViewer.Size = New System.Drawing.Size(454, 526)
            Me._colorViewer.TabIndex = 4
            Me._colorViewer.Text = "_colorViewer"
            '			Me._colorViewer.MouseMovePixel += New System.Windows.Forms.MouseEventHandler(Me._colorViewer_MouseMovePixel);
            Me._colorViewer.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.Magnifier
            ' 
            ' Form1
            ' 
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(920, 617)
            Me.Controls.Add(Me.panelColor)
            Me.Controls.Add(Me.panelOriginal)
            Me.Controls.Add(Me.lblColorLabel)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.toolBar1)
            Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
            Me.Menu = Me.mainMenu1
            Me.MinimumSize = New System.Drawing.Size(384, 288)
            Me.Name = "Form1"
            Me.Text = "Color Extraction Demo"
            '			Me.Resize += New System.EventHandler(Me.Form1_Resize);
            '			Me.Closing += New System.ComponentModel.CancelEventHandler(Me.Form1_Closing);
            CType(Me.sbpImageFile, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.sbpProcessingTime, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.sbpPixelColor, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panelOriginal.ResumeLayout(False)
            Me.panelColor.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.DoEvents()
            Application.Run(New Form1())
        End Sub

#Region "Private Methods"


        ''' <summary>Clears the Image Viewers and disposes the images.</summary>
        Private Sub DisposeImages()

            If Not Me._colorViewer.Image Is Nothing Then
                Me._colorViewer.Image.Dispose()
                Me._colorViewer.Image = Nothing
            End If

            If Not Me._originalViewer.Image Is Nothing Then
                Me._originalViewer.Image.Dispose()
                Me._originalViewer.Image = Nothing
            End If
        End Sub

        ''' <summary>Displays a MessageBox with an Error icon.</summary>
        ''' <param name="title">The title for the MessageBox.</param>
        ''' <param name="message">
        ''' The initial message to display. If the <em>ex</em> parameter is set the message
        ''' will include additional content.
        ''' </param>
        ''' <param name="ex">
        ''' The exception that was thrown or <strong>null</strong> (<strong>Nothing</strong>
        ''' in VB) if there was no exception.
        ''' </param>
        Private Sub ShowErrorDialog(ByVal title As String, ByVal message As String, ByVal ex As System.Exception)
            If Not ex Is Nothing Then
                message &= (Constants.vbCrLf & "Exception:  " & ex.Message)
                If Not ex.InnerException Is Nothing Then
                    message &= (Constants.vbCrLf & "Additional Information:  " & ex.InnerException.Message)
                End If
            End If

            MessageBox.Show(Me, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

        ''' <summary>Displays a SaveFileDialog and returns the filename without an extension.</summary>
        ''' <returns>The filename provided without its extension.</returns>
        Private Function GetSaveFileName() As String
            'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
            '			using (SaveFileDialog dlg = New SaveFileDialog())
            Dim dlg As SaveFileDialog = New SaveFileDialog
            Try
                dlg.Title = "Save Image(s)"
                'dlg.Filter = "Images (*.tif, *.jp2, *.jb2)|*.tif;*.jp2;*.jb2"
                dlg.Filter = HelperMethods.CreateDialogFilter(False)
                If dlg.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    ' Return the filename without an extension because it will
                    ' be added based on the type of image being saved.
                    Return System.IO.Path.GetFileNameWithoutExtension(dlg.FileName)
                Else
                    Return Nothing
                End If
            Finally
                CType(dlg, IDisposable).Dispose()
            End Try
            'INSTANT VB NOTE: End of the original C# 'using' block
        End Function

        ''' <summary>Updates the menu and toolbar items.</summary>
        Private Sub UpdateMenusAndToolbar()
            Me.menuCommandProcess.Enabled = Not Me._originalViewer.Image Is Nothing
            Me.tbProcess.Enabled = Me.menuCommandProcess.Enabled
        End Sub

        ''' <summary>
        ''' Displays an OpenFileDialog and loads the image into the main
        ''' ImageViewer.
        ''' </summary>
        Private Sub OpenImage()
            Dim dlg As OpenFileDialog = New OpenFileDialog
            'dlg.Filter = "Images (*.tif, *.jpg, *.png, *.bmp)|*.tif;*.jpg;*.png;*.bmp"
            dlg.Filter = HelperMethods.CreateDialogFilter(True)
            Try
                If dlg.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    Me.Cursor = Cursors.WaitCursor
                    DisposeImages()
                    Me._originalViewer.Image = New AtalaImage(dlg.FileName)

                    UpdateMenusAndToolbar()
                    Me.sbpImageFile.Text = "File:  " & System.IO.Path.GetFileName(dlg.FileName)
                End If
            Catch ex As System.Exception
                ShowErrorDialog("Image Read Failed", "There was an error loading the image.", ex)
            Finally
                dlg.Dispose()
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        ''' <summary>Applies the ColorRegionDetectionCommand to the image.</summary>
        Private Sub ProcessCommand()
            If Me._originalViewer.Image Is Nothing Then
                MessageBox.Show(Me, "Please load an image before processing.", "No Image")
                Return
            End If

            Try
                Dim tick As Integer = System.Environment.TickCount
                Me.Cursor = Cursors.WaitCursor
                Dim results As ColorExtractionResults = CType(Me._command.Apply(Me._originalViewer.Image), ColorExtractionResults)

                ' Dispose any previous images.
                If Not Me._colorViewer.Image Is Nothing Then
                    Me._colorViewer.Image.Dispose()
                    Me._colorViewer.Image = Nothing
                End If

                Me.sbpProcessingTime.Text = "Processing Time: " & (System.Environment.TickCount - tick) & " ms"

                If (Not results.HasColor) Then
                    MessageBox.Show(Me, "No color was detected in this image", "Processing Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Me._colorViewer.Image = results.Image
                End If

                UpdateMenusAndToolbar()
            Catch ex As System.Exception
                ShowErrorDialog("Process Error", "There was an error processing the image.", ex)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        ''' <summary>
        ''' Displays a dialog that will allow the ColorRegionDetectionCommand property values
        ''' to be modified.
        ''' </summary>
        Private Sub ShowCommandSettings()
            ' Pass in a copy of the command in case they cancel changes.
            Dim cmd As ColorExtractionCommand = New ColorExtractionCommand

            cmd.ApplyToAnyPixelFormat = Me._command.ApplyToAnyPixelFormat
            cmd.MinMarkingSize = _command.MinMarkingSize
            cmd.MinPhotoSize = _command.MinPhotoSize
            cmd.VisualActivityWindowSize = _command.VisualActivityWindowSize
            cmd.SpeedFactor = _command.SpeedFactor
            cmd.GrayscaleSaturation = _command.GrayscaleSaturation
            cmd.GrayscaleTolerance = _command.GrayscaleTolerance
            cmd.VisualActivityThreshold = _command.VisualActivityThreshold
            cmd.DetectColorMarkings = _command.DetectColorMarkings
            cmd.DetectPhotos = _command.DetectPhotos

            Dim frm As CommandSettings = New CommandSettings(cmd)
            Try
                If frm.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    Me._command = cmd
                End If
            Catch ex As System.Exception
                ShowErrorDialog("Setting Properties Failed", "There was an error while changing the command properties.", ex)
            Finally
                frm.Dispose()
            End Try
        End Sub

        ''' <summary>Displays the About dialog.</summary>
        Private Sub ShowAboutDialog()
            Dim frm As AtalaDemos.AboutBox.About = New AtalaDemos.AboutBox.About("About...", "Color Extraction Demo")
            frm.Description = "This application provides a demonstration of the ColorExtractionCommand included in the Atalasoft DotImage Advanced Document Cleanup module." & Constants.vbCrLf & Constants.vbCrLf & "This command is used to detect color in a color image, and returns a 32-bit BGRA image with the alpha channel covering the non-color regions.  This can be used to determine if a scanned image is actually grayscale, in which case the image can be thresholded to B&W and saved using CCIT or JBIG2 compression, or saved as 8-bit grayscale instead of 24-bit color."
            frm.ShowDialog(Me)
            frm.Dispose()
        End Sub

#End Region

#Region "File Menu"

        Private Sub menuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuExit.Click
            Me.Close()
        End Sub

        Private Sub menuOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpen.Click
            OpenImage()
        End Sub

#End Region

#Region "Command Menu"

        Private Sub menuCommandSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCommandSettings.Click
            ShowCommandSettings()
        End Sub

        Private Sub menuCommandProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCommandProcess.Click
            ProcessCommand()
        End Sub

#End Region

#Region "Help Menu"

        Private Sub menuHelpAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuHelpAbout.Click
            ShowAboutDialog()
        End Sub

#End Region

#Region "Toolbar"

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case e.Button.ToolTipText
                Case "Open"
                    OpenImage()
                Case "Command Settings"
                    ShowCommandSettings()
                Case "Process Command"
                    ProcessCommand()
                Case "About..."
                    ShowAboutDialog()
            End Select
        End Sub

#End Region

#Region "Form Events"

        Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
            DisposeImages()
        End Sub

        Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
            Dim halfSize As Integer = Me.ClientSize.Width / 2
            Me.panelOriginal.Width = halfSize - 10
            Me.panelColor.Width = halfSize - 10
            Me.panelColor.Location = New Point(halfSize + 2, panelOriginal.Top)
            Me.lblColorLabel.Left = Me.panelColor.Location.X
        End Sub

#End Region

        Private Sub _originalViewer_MouseMovePixel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _originalViewer.MouseMovePixel
            Me.sbpPixelColor.Text = _originalViewer.Image.GetPixelColor(e.X, e.Y).ToString()
        End Sub

        Private Sub _colorViewer_MouseMovePixel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _colorViewer.MouseMovePixel
            Me.sbpPixelColor.Text = _colorViewer.Image.GetPixelColor(e.X, e.Y).ToString()
        End Sub

    End Class
End Namespace
