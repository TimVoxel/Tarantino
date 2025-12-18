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
            _addResponseButton = new Button();
            _textView = new DataGridView();
            _answerResponseEditPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 0);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(63, 37);
            label2.TabIndex = 2;
            label2.Text = "Text";
            // 
            // _answerResponseEditPanel
            // 
            _answerResponseEditPanel.Controls.Add(label2);
            _answerResponseEditPanel.Controls.Add(_textView);
            _answerResponseEditPanel.Controls.Add(_addResponseButton);
            _answerResponseEditPanel.Dock = DockStyle.Fill;
            _answerResponseEditPanel.Location = new Point(0, 0);
            _answerResponseEditPanel.Margin = new Padding(6, 7, 6, 7);
            _answerResponseEditPanel.Name = "_answerResponseEditPanel";
            _answerResponseEditPanel.Size = new Size(1003, 387);
            _answerResponseEditPanel.TabIndex = 1;
            // 
            // _addResponseButton
            // 
            _addResponseButton.Location = new Point(6, 268);
            _addResponseButton.Margin = new Padding(6, 7, 6, 7);
            _addResponseButton.Name = "_addResponseButton";
            _addResponseButton.Size = new Size(971, 57);
            _addResponseButton.TabIndex = 4;
            _addResponseButton.Text = "Add response";
            _addResponseButton.UseVisualStyleBackColor = true;
            _addResponseButton.Click += OnAddResponseClicked;
            // 
            // listView1
            // 
            _textView.Location = new Point(3, 40);
            _textView.Name = "_textComponentListView";
            _textView.Size = new Size(974, 218);
            _textView.TabIndex = 5;
            _textView.CellValueChanged += OnTextComponentCellValueChanged;
            _textView.AllowUserToAddRows = true;
           
            // 
            // DialogEditor
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = false;
            Controls.Add(_answerResponseEditPanel);
            Margin = new Padding(6, 7, 6, 7);
            Name = "DialogEditor";
            Size = new Size(1003, 387);
            _answerResponseEditPanel.ResumeLayout(false);
            _answerResponseEditPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label2;
        private Panel _answerResponseEditPanel;
        private Button _addResponseButton;
        private DataGridView _textView;
    }
}
