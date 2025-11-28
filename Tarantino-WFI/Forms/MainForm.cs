namespace Tarantino.WFI
{
    public partial class MainForm : Form
    {
        private IDialogNodeEditor[] _nodeEditors;

        public MainForm()
        {
            InitializeComponent();

            _nodeEditors = [
                _answerResponseEditor,
                _subDialogResponseEditor,
                _dialogEditor
            ];

            HidePanels();
        }

        private void HidePanels()
        {
            foreach (var editor in _nodeEditors)
            {
                editor.Hide();
            }       
        }

        private void OnImportDialogClicked(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Tarantino Dialog File|*.json";
                openFileDialog.Title = "Import Dialog File";

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var filePath = openFileDialog.FileName;
                var json = File.ReadAllText(filePath);
                var dialog = Dialog.FromJson(json);
                var builder = _dialogTreeView.LoadDialog(dialog);
                StartEditing(_dialogTreeView.Root, builder);
            }
        }

        private void OnExportDialogClicked(object sender, EventArgs e)
        {
            if (!_dialogTreeView.HasLoadedDialog)
            {
                MessageBox.Show("No dialog loaded, nothing to export",
                                "Export error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Tarantino Dialog File|*.json";
                saveFileDialog.Title = "Export Dialog File";

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                var filePath = saveFileDialog.FileName;
                var json = _dialogTreeView.BuildDialog().ToJson();
                File.WriteAllText(filePath, json);
            }
        }

        private void StartEditing(DialogTreeNode? selectedNode, DialogNode.Builder? builder)
        {
            if (selectedNode == null)
            {
                HidePanels();
                return;
            }
         
            foreach (var editor in _nodeEditors)
            {
                if (editor.TargetKind == builder?.Kind)
                {
                    editor.Show();
                    editor.LoadBuilder(builder);
                }
                else
                {
                    editor.Hide();
                }
            }
        }
        
        private void HandleNodeChanges(DialogNode.Builder builder)
        {
            var selected = _dialogTreeView.SelectedNode;

            if (selected != null)
            {
                _dialogTreeView.RefreshNode(selected, builder);
                StartEditing(selected, builder);
            }
        }

        private void AddDialogResponseNode(DialogNode.Builder builder)
            => _dialogTreeView.AddDialogResponse(_dialogTreeView.SelectedNode!, builder);
        
        private void OnEditSubDialogRequested(Dialog.DialogBuilder builder)
        {
            _dialogTreeView.SelectedNode = (_dialogTreeView.SelectedNode!.Nodes[0] as DialogTreeNode) ?? throw new Exception("Edit dialog clicked while editing non-subdialog node");
        }
    }
}
