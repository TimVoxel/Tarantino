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
            _rootPanel = new FlowLayoutPanel();
            _handle = new DataGridView();
            _rootPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_handle).BeginInit();
            SuspendLayout();
            // 
            // _answerResponseEditPanel
            // 
            _rootPanel.Controls.Add(_handle);
            _rootPanel.Dock = DockStyle.Fill;
            _rootPanel.FlowDirection = FlowDirection.TopDown;
            _rootPanel.Location = new Point(0, 0);
            _rootPanel.Margin = new Padding(6, 7, 6, 7);
            _rootPanel.Name = "_answerResponseEditPanel";
            _rootPanel.Size = new Size(1003, 569);
            _rootPanel.TabIndex = 1;
            // 
            // dataGridView1
            // 
            _handle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _handle.Dock = DockStyle.Left;
            _handle.Location = new Point(3, 3);
            _handle.Name = "dataGridView1";
            _handle.RowHeadersWidth = 92;
            _handle.Size = new Size(997, 561);
            _handle.TabIndex = 0;
            // 
            // EventEditor
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(_rootPanel);
            Margin = new Padding(6, 7, 6, 7);
            Name = "EventEditor";
            Size = new Size(1003, 569);
            _rootPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_handle).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel _rootPanel;
        private DataGridView _handle;
    }
}
