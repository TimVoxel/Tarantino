


namespace Tarantino.WFI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            _dialogTreeView = new DialogTreeView();
            _dialogEditor = new DialogEditor();
            _subDialogResponseEditor = new SubDialogResponseEditor();
            _answerResponseEditor = new AnswerResponseEditor();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            dialogToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Margin = new Padding(1);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(_dialogTreeView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_dialogEditor);
            splitContainer1.Panel2.Controls.Add(_subDialogResponseEditor);
            splitContainer1.Panel2.Controls.Add(_answerResponseEditor);
            splitContainer1.Size = new Size(696, 364);
            splitContainer1.SplitterDistance = 230;
            splitContainer1.SplitterWidth = 2;
            splitContainer1.TabIndex = 2;
            // 
            // _dialogTreeView
            // 
            _dialogTreeView.Dock = DockStyle.Fill;
            _dialogTreeView.Location = new Point(0, 0);
            _dialogTreeView.Margin = new Padding(0);
            _dialogTreeView.Name = "_dialogTreeView";
            _dialogTreeView.SelectedNode = null;
            _dialogTreeView.Size = new Size(230, 364);
            _dialogTreeView.TabIndex = 0;
            _dialogTreeView.NodeSelected += StartEditing;
            // 
            // _dialogEditor
            // 
            _dialogEditor.AutoSize = true;
            _dialogEditor.Location = new Point(0, 0);
            _dialogEditor.Name = "_dialogEditor";
            _dialogEditor.Size = new Size(468, 297);
            _dialogEditor.TabIndex = 2;
            _dialogEditor.ChangesMade += HandleNodeChanges;
            _dialogEditor.ResponseAdded += AddDialogResponseNode;
            // 
            // _subDialogResponseEditor
            // 
            _subDialogResponseEditor.AutoSize = true;
            _subDialogResponseEditor.Dock = DockStyle.Fill;
            _subDialogResponseEditor.Location = new Point(0, 0);
            _subDialogResponseEditor.Name = "_subDialogResponseEditor";
            _subDialogResponseEditor.Size = new Size(464, 364);
            _subDialogResponseEditor.TabIndex = 1;
            _subDialogResponseEditor.EditSubDialogRequested += OnEditSubDialogRequested;
            _subDialogResponseEditor.ChangesMade += HandleNodeChanges;
            // 
            // _answerResponseEditor
            // 
            _answerResponseEditor.AutoSize = true;
            _answerResponseEditor.Dock = DockStyle.Fill;
            _answerResponseEditor.Location = new Point(0, 0);
            _answerResponseEditor.Name = "_answerResponseEditor";
            _answerResponseEditor.Size = new Size(464, 364);
            _answerResponseEditor.TabIndex = 0;
            _answerResponseEditor.ChangesMade += HandleNodeChanges;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(696, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, importToolStripMenuItem, exportToolStripMenuItem, saveToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { dialogToolStripMenuItem });
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(180, 22);
            newToolStripMenuItem.Text = "New";
            // 
            // dialogToolStripMenuItem
            // 
            dialogToolStripMenuItem.Name = "dialogToolStripMenuItem";
            dialogToolStripMenuItem.Size = new Size(108, 22);
            dialogToolStripMenuItem.Text = "Dialog";
            dialogToolStripMenuItem.Click += OnNewDialogClicked;
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(180, 22);
            importToolStripMenuItem.Text = "Import";
            importToolStripMenuItem.Click += OnImportDialogClicked;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(180, 22);
            exportToolStripMenuItem.Text = "Export";
            exportToolStripMenuItem.Click += OnExportDialogClicked;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(180, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += OnSaveClicked;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(696, 388);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(1);
            KeyPreview = true;
            KeyDown += OnKeyPressed;
            Name = "MainForm";
            Text = "Tarantino WFI";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private SplitContainer splitContainer1;
        private DialogTreeView _dialogTreeView;
        private AnswerResponseEditor _answerResponseEditor;
        private SubDialogResponseEditor _subDialogResponseEditor;
        private DialogEditor _dialogEditor;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem dialogToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
    }
}
