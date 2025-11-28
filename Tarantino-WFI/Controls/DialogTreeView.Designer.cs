namespace Tarantino.WFI
{
    partial class DialogTreeView
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
            _handle = new TreeView();
            SuspendLayout();
            // 
            // _handle
            // 
            _handle.Dock = DockStyle.Fill;
            _handle.LabelEdit = true;
            _handle.Location = new Point(0, 0);
            _handle.Margin = new Padding(1, 1, 1, 1);
            _handle.Name = "_handle";
            _handle.Size = new Size(343, 315);
            _handle.TabIndex = 0;
            _handle.AfterSelect += OnNodeSelected;
            _handle.LabelEdit = false;
            _handle.AllowDrop = true;
            _handle.ItemDrag += OnItemDrag;
            _handle.DragEnter += OnDragEnter;
            _handle.DragOver += OnDragOver;
            _handle.DragDrop += OnDragDrop;
            _handle.KeyDown += OnTreeKeyDown;
            // 
            // DialogTreeView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_handle);
            Margin = new Padding(1, 1, 1, 1);
            Name = "DialogTreeView";
            Size = new Size(343, 315);
            ResumeLayout(false);
        }

        #endregion

        private TreeView _handle;
    }
}
