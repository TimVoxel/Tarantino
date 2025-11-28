namespace Tarantino.WFI
{
    partial class SubDialogResponseEditor
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
            label1 = new Label();
            _typeComboBox = new ComboBox();
            label2 = new Label();
            _textTextBox = new RichTextBox();
            _answerResponseEditPanel = new FlowLayoutPanel();
            button1 = new Button();
            _answerResponseEditPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 1;
            label1.Text = "Type";
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
            // _answerResponseEditPanel
            // 
            _answerResponseEditPanel.Controls.Add(label1);
            _answerResponseEditPanel.Controls.Add(_typeComboBox);
            _answerResponseEditPanel.Controls.Add(label2);
            _answerResponseEditPanel.Controls.Add(_textTextBox);
            _answerResponseEditPanel.Controls.Add(button1);
            _answerResponseEditPanel.Dock = DockStyle.Fill;
            _answerResponseEditPanel.FlowDirection = FlowDirection.TopDown;
            _answerResponseEditPanel.Location = new Point(0, 0);
            _answerResponseEditPanel.Name = "_answerResponseEditPanel";
            _answerResponseEditPanel.Size = new Size(468, 297);
            _answerResponseEditPanel.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(3, 164);
            button1.Name = "button1";
            button1.Size = new Size(453, 23);
            button1.TabIndex = 4;
            button1.Text = "Edit sub dialog";
            button1.UseVisualStyleBackColor = true;
            button1.Click += OnEditSubDialogClicked;
            // 
            // SubDialogResponseEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(_answerResponseEditPanel);
            Name = "SubDialogResponseEditor";
            Size = new Size(468, 297);
            _answerResponseEditPanel.ResumeLayout(false);
            _answerResponseEditPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private ComboBox _typeComboBox;
        private Label label2;
        private RichTextBox _textTextBox;
        private FlowLayoutPanel _answerResponseEditPanel;
        private Button button1;
    }
}
