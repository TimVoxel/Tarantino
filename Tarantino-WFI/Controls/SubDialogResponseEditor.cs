namespace Tarantino.WFI
{
    public partial class SubDialogResponseEditor : UserControl, IDialogNodeEditor
    {
        private SubDialogResponse.SubDialogBuilder? _loadedBuilder;
        public DialogNodeKind TargetKind => DialogNodeKind.SubDialogResponse;

        public event Action<DialogNode.Builder>? TypeChanged;
        public event Action<Dialog.DialogBuilder>? EditSubDialogRequested;
        public event Action<DialogNode.Builder>? ChangesMade;

        public SubDialogResponseEditor()
        {
            InitializeComponent();
            _typeComboBox.DataSource = DialogFacts.ResponseDialogKinds.ToArray();
        }

        public void LoadBuilder(DialogNode.Builder builder)
        {
            if (builder is not SubDialogResponse.SubDialogBuilder answerBuilder)
            {
                throw new InvalidOperationException("Failed to convert builder to AnswerDialogResponse.AnswerBuilder.");
            }

            _loadedBuilder = answerBuilder;
            _typeComboBox.SelectedItem = builder.Kind;
            _textTextBox.Text = answerBuilder.Text;
        }

        private void OnTypeChanged(object sender, EventArgs e)
        {
            if (_loadedBuilder != null)
            {
                var newKind = (DialogNodeKind)_typeComboBox.SelectedItem!;
                var converted = _loadedBuilder.ConvertToKind(newKind);
                TypeChanged?.Invoke(converted);
                ChangesMade?.Invoke(converted);
            }
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (_loadedBuilder != null)
            {
                _loadedBuilder.Text = _textTextBox.Text;
                ChangesMade?.Invoke(_loadedBuilder);
            }
        }

        private void OnEditSubDialogClicked(object sender, EventArgs e)
        {
            if (_loadedBuilder != null)
            {
                EditSubDialogRequested?.Invoke(_loadedBuilder.Dialog);
            }
        }
    }
}
