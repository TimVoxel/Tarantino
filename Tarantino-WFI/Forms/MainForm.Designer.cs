

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
            _controlButtonPanel = new FlowLayoutPanel();
            _importDialogButton = new Button();
            _exportDialogButton = new Button();
            splitContainer1 = new SplitContainer();
            _dialogTreeView = new DialogTreeView();
            _subDialogResponseEditor = new SubDialogResponseEditor();
            _answerResponseEditor = new AnswerResponseEditor();
            _dialogEditor = new DialogEditor();
            _controlButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // _controlButtonPanel
            // 
            _controlButtonPanel.Controls.Add(_importDialogButton);
            _controlButtonPanel.Controls.Add(_exportDialogButton);
            _controlButtonPanel.Dock = DockStyle.Top;
            _controlButtonPanel.Location = new Point(0, 0);
            _controlButtonPanel.Margin = new Padding(1);
            _controlButtonPanel.Name = "_controlButtonPanel";
            _controlButtonPanel.Size = new Size(696, 26);
            _controlButtonPanel.TabIndex = 1;
            // 
            // _importDialogButton
            // 
            _importDialogButton.Location = new Point(1, 1);
            _importDialogButton.Margin = new Padding(1);
            _importDialogButton.Name = "_importDialogButton";
            _importDialogButton.Size = new Size(111, 21);
            _importDialogButton.TabIndex = 0;
            _importDialogButton.Text = "Import";
            _importDialogButton.UseVisualStyleBackColor = true;
            _importDialogButton.Click += OnImportDialogClicked;
            // 
            // _exportDialogButton
            // 
            _exportDialogButton.Location = new Point(114, 1);
            _exportDialogButton.Margin = new Padding(1);
            _exportDialogButton.Name = "_exportDialogButton";
            _exportDialogButton.Size = new Size(111, 21);
            _exportDialogButton.TabIndex = 1;
            _exportDialogButton.Text = "Export";
            _exportDialogButton.UseVisualStyleBackColor = true;
            _exportDialogButton.Click += OnExportDialogClicked;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 26);
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
            splitContainer1.Size = new Size(696, 362);
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
            _dialogTreeView.Size = new Size(230, 362);
            _dialogTreeView.TabIndex = 0;
            _dialogTreeView.NodeSelected += StartEditing;
            // 
            // _subDialogResponseEditor
            // 
            _subDialogResponseEditor.AutoSize = true;
            _subDialogResponseEditor.Dock = DockStyle.Fill;
            _subDialogResponseEditor.Location = new Point(0, 0);
            _subDialogResponseEditor.Name = "_subDialogResponseEditor";
            _subDialogResponseEditor.Size = new Size(464, 362);
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
            _answerResponseEditor.Size = new Size(464, 362);
            _answerResponseEditor.TabIndex = 0;
            _answerResponseEditor.ChangesMade += HandleNodeChanges;
            // 
            // dialogEditor1
            // 
            _dialogEditor.AutoSize = true;
            _dialogEditor.Location = new Point(0, 0);
            _dialogEditor.Name = "_dialogEditor";
            _dialogEditor.Size = new Size(468, 297);
            _dialogEditor.TabIndex = 2;
            _dialogEditor.ResponseAdded += AddDialogResponseNode;
            _dialogEditor.ChangesMade += HandleNodeChanges;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(696, 388);
            Controls.Add(splitContainer1);
            Controls.Add(_controlButtonPanel);
            Margin = new Padding(1);
            Name = "MainForm";
            Text = "Tarantino WFI";
            _controlButtonPanel.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel _controlButtonPanel;
        private Button _importDialogButton;
        private Button _exportDialogButton;
        private SplitContainer splitContainer1;
        private DialogTreeView _dialogTreeView;
        private AnswerResponseEditor _answerResponseEditor;
        private SubDialogResponseEditor _subDialogResponseEditor;
        private DialogEditor _dialogEditor;
    }
}
