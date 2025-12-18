namespace Tarantino.WFI
{
    partial class EventEditor
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
            _typeComboBox = new ComboBox();
            label2 = new Label();
            _textTextBox = new RichTextBox();
            label3 = new Label();
            _answerTextBox = new RichTextBox();
            _answerResponseEditPanel = new FlowLayoutPanel();
            _answerResponseEditPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _typeComboBox
            // 
            _typeComboBox.FormattingEnabled = true;
            _typeComboBox.Location = new Point(3, 18);
            _typeComboBox.Name = "_typeComboBox";
            _typeComboBox.Size = new Size(121, 23);
            _typeComboBox.TabIndex = 0;
            _typeComboBox.SelectedIndexChanged += OnTypeChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 44);
            label2.Name = "label2";
            label2.Size = new Size(28, 15);
            label2.TabIndex = 2;
            label2.Text = "Text";
            // 
            // _textTextBox
            // 
            _textTextBox.Dock = DockStyle.Left;
            _textTextBox.Location = new Point(3, 62);
            _textTextBox.Name = "_textTextBox";
            _textTextBox.Size = new Size(453, 96);
            _textTextBox.TabIndex = 3;
            _textTextBox.Text = "";
            _textTextBox.TextChanged += OnTextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 161);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 4;
            label3.Text = "Answer";
            // 
            // _answerTextBox
            // 
            _answerTextBox.Dock = DockStyle.Left;
            _answerTextBox.Location = new Point(3, 179);
            _answerTextBox.Name = "_answerTextBox";
            _answerTextBox.Size = new Size(453, 96);
            _answerTextBox.TabIndex = 5;
            _answerTextBox.Text = "";
            _answerTextBox.TextChanged += OnAnswerChanged;
            // 
            // _answerResponseEditPanel
            //
            _answerResponseEditPanel.Controls.Add(_typeComboBox);
            _answerResponseEditPanel.Controls.Add(label2);
            _answerResponseEditPanel.Controls.Add(_textTextBox);
            _answerResponseEditPanel.Controls.Add(label3);
            _answerResponseEditPanel.Controls.Add(_answerTextBox);
            _answerResponseEditPanel.Dock = DockStyle.Fill;
            _answerResponseEditPanel.FlowDirection = FlowDirection.TopDown;
            _answerResponseEditPanel.Location = new Point(0, 0);
            _answerResponseEditPanel.Name = "_answerResponseEditPanel";
            _answerResponseEditPanel.Size = new Size(468, 297);
            _answerResponseEditPanel.TabIndex = 1;
            // 
            // AnswerResponseEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(_answerResponseEditPanel);
            Name = "AnswerResponseEditor";
            Size = new Size(468, 297);
            _answerResponseEditPanel.ResumeLayout(false);
            _answerResponseEditPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox _typeComboBox;
        private Label label2;
        private RichTextBox _textTextBox;
        private Label label3;
        private RichTextBox _answerTextBox;
        private FlowLayoutPanel _answerResponseEditPanel;
    }
}
