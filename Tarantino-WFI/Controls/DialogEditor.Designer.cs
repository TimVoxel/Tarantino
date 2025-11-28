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
            _textTextBox = new RichTextBox();
            _answerResponseEditPanel = new FlowLayoutPanel();
            _addResponseButton = new Button();
            _answerResponseEditPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(28, 15);
            label2.TabIndex = 2;
            label2.Text = "Text";
            // 
            // _textTextBox
            // 
            _textTextBox.Dock = DockStyle.Left;
            _textTextBox.Location = new Point(3, 18);
            _textTextBox.Name = "_textTextBox";
            _textTextBox.Size = new Size(453, 96);
            _textTextBox.TabIndex = 3;
            _textTextBox.Text = "";
            _textTextBox.TextChanged += OnTextChanged;
            // 
            // _answerResponseEditPanel
            // 
            _answerResponseEditPanel.Controls.Add(label2);
            _answerResponseEditPanel.Controls.Add(_textTextBox);
            _answerResponseEditPanel.Controls.Add(_addResponseButton);
            _answerResponseEditPanel.Dock = DockStyle.Fill;
            _answerResponseEditPanel.FlowDirection = FlowDirection.TopDown;
            _answerResponseEditPanel.Location = new Point(0, 0);
            _answerResponseEditPanel.Name = "_answerResponseEditPanel";
            _answerResponseEditPanel.Size = new Size(468, 157);
            _answerResponseEditPanel.TabIndex = 1;
            // 
            // _addResponseButton
            // 
            _addResponseButton.Location = new Point(3, 120);
            _addResponseButton.Name = "_addResponseButton";
            _addResponseButton.Size = new Size(453, 23);
            _addResponseButton.TabIndex = 4;
            _addResponseButton.Text = "Add response";
            _addResponseButton.UseVisualStyleBackColor = true;
            _addResponseButton.Click += OnAddResponseClicked;
            // 
            // DialogEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(_answerResponseEditPanel);
            Name = "DialogEditor";
            Size = new Size(468, 157);
            _answerResponseEditPanel.ResumeLayout(false);
            _answerResponseEditPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label2;
        private RichTextBox _textTextBox;
        private FlowLayoutPanel _answerResponseEditPanel;
        private Button _addResponseButton;
    }
}
