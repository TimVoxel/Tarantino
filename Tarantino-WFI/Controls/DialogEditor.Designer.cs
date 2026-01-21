using System.Windows.Forms;

namespace Tarantino.WFI
{
    partial class DialogEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label2 = new Label();
            _answerResponseEditPanel = new Panel();
            _eventEditor = new EventEditor();
            _textView = new DataGridView();
            _addResponseButton = new Button();
            _answerResponseEditPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_textView).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Location = new Point(0, 0);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(63, 37);
            label2.TabIndex = 2;
            label2.Text = "Text";
            // 
            // _answerResponseEditPanel
            // 
            _answerResponseEditPanel.Controls.Add(_eventEditor);
            _answerResponseEditPanel.Controls.Add(label2);
            _answerResponseEditPanel.Controls.Add(_textView);
            _answerResponseEditPanel.Controls.Add(_addResponseButton);
            _answerResponseEditPanel.Dock = DockStyle.Fill;
            _answerResponseEditPanel.Location = new Point(0, 0);
            _answerResponseEditPanel.Margin = new Padding(6, 7, 6, 7);
            _answerResponseEditPanel.Name = "_answerResponseEditPanel";
            _answerResponseEditPanel.Size = new Size(1003, 725);
            _answerResponseEditPanel.TabIndex = 1;
            // 
            // _eventEditor
            // 
            _eventEditor.AutoSize = true;
            _eventEditor.Location = new Point(6, 339);
            _eventEditor.Margin = new Padding(6, 7, 6, 7);
            _eventEditor.Name = "_eventEditor";
            _eventEditor.Size = new Size(991, 379);
            _eventEditor.TabIndex = 6;
            // 
            // _textView
            // 
            _textView.ColumnHeadersHeight = 52;
            _textView.Location = new Point(3, 40);
            _textView.Name = "_textView";
            _textView.RowHeadersWidth = 92;
            _textView.Size = new Size(997, 218);
            _textView.TabIndex = 5;
            _textView.CellValueChanged += OnTextComponentCellValueChanged;
            // 
            // _addResponseButton
            // 
            _addResponseButton.Location = new Point(6, 268);
            _addResponseButton.Margin = new Padding(6, 7, 6, 7);
            _addResponseButton.Name = "_addResponseButton";
            _addResponseButton.Size = new Size(991, 57);
            _addResponseButton.TabIndex = 4;
            _addResponseButton.Text = "Add response";
            _addResponseButton.UseVisualStyleBackColor = true;
            _addResponseButton.Click += OnAddResponseClicked;
            // 
            // DialogEditor
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_answerResponseEditPanel);
            Margin = new Padding(6, 7, 6, 7);
            Name = "DialogEditor";
            Size = new Size(1003, 725);
            _answerResponseEditPanel.ResumeLayout(false);
            _answerResponseEditPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_textView).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label label2;
        private Panel _answerResponseEditPanel;
        private Button _addResponseButton;
        private DataGridView _textView;
        private EventEditor _eventEditor;
    }
}
