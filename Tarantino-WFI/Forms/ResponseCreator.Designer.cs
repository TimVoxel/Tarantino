namespace Tarantino.WFI.Forms
{
    partial class ResponseCreator
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _layoutPanel = new FlowLayoutPanel();
            label1 = new Label();
            _typeComboBox = new ComboBox();
            label2 = new Label();
            _textTextBox = new RichTextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            _cancelButton = new Button();
            _doneButton = new Button();
            _layoutPanel.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // _layoutPanel
            // 
            _layoutPanel.Controls.Add(label1);
            _layoutPanel.Controls.Add(_typeComboBox);
            _layoutPanel.Controls.Add(label2);
            _layoutPanel.Controls.Add(_textTextBox);
            _layoutPanel.Controls.Add(flowLayoutPanel1);
            _layoutPanel.Dock = DockStyle.Fill;
            _layoutPanel.FlowDirection = FlowDirection.TopDown;
            _layoutPanel.Location = new Point(0, 0);
            _layoutPanel.Name = "_layoutPanel";
            _layoutPanel.Size = new Size(463, 213);
            _layoutPanel.TabIndex = 2;
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
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(_cancelButton);
            flowLayoutPanel1.Controls.Add(_doneButton);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(3, 164);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(453, 38);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // _cancelButton
            // 
            _cancelButton.Location = new Point(3, 3);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.Size = new Size(215, 35);
            _cancelButton.TabIndex = 0;
            _cancelButton.Text = "Cancel";
            _cancelButton.UseVisualStyleBackColor = true;
            _cancelButton.Click += OnCancelButtonClicked;
            // 
            // _doneButton
            // 
            _doneButton.Location = new Point(224, 3);
            _doneButton.Name = "_doneButton";
            _doneButton.Size = new Size(225, 35);
            _doneButton.TabIndex = 1;
            _doneButton.Text = "Done";
            _doneButton.UseVisualStyleBackColor = true;
            _doneButton.Click += OnDoneButtonClicked;
            // 
            // ResponseCreator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(463, 213);
            Controls.Add(_layoutPanel);
            Name = "ResponseCreator";
            Text = "Add response";
            _layoutPanel.ResumeLayout(false);
            _layoutPanel.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel _layoutPanel;
        private Label label1;
        private ComboBox _typeComboBox;
        private Label label2;
        private RichTextBox _textTextBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button _cancelButton;
        private Button _doneButton;
    }
}