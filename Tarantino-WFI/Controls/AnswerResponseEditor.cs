namespace Tarantino.WFI
{
    public partial class AnswerResponseEditor : UserControl, IDialogNodeEditor
    {
        private AnswerDialogResponse.AnswerBuilder? _loadedBuilder;
        public DialogNodeKind TargetKind => DialogNodeKind.AnswerResponse;

        public event Action<DialogNode.Builder>? ChangesMade;

        public AnswerResponseEditor()
        {
            InitializeComponent();
            _typeComboBox.DataSource = DialogFacts.ResponseDialogKinds.ToArray();
        }

        public void LoadBuilder(DialogNode.Builder builder)
        {
            if (builder is not AnswerDialogResponse.AnswerBuilder answerBuilder)
            {
                throw new InvalidOperationException("Failed to convert builder to AnswerDialogResponse.AnswerBuilder.");
            }

            _loadedBuilder = answerBuilder;
            _typeComboBox.SelectedItem = builder.Kind;
            _textTextBox.Text = answerBuilder.Text;
            _answerTextBox.Text = answerBuilder.Answer ?? string.Empty;
        }

        private void OnTypeChanged(object sender, EventArgs e)
        {
            if (_loadedBuilder != null)
            {
                var newKind = (DialogNodeKind)_typeComboBox.SelectedItem!;
                var converted = _loadedBuilder.ConvertToKind(newKind);
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

        private void OnAnswerChanged(object sender, EventArgs e)
        {
            if (_loadedBuilder != null)
            {   
                var text = _answerTextBox.Text;

                _loadedBuilder.Answer = !string.IsNullOrWhiteSpace(text)
                    ? text
                    : null;

                ChangesMade?.Invoke(_loadedBuilder);
            }
        }
    }
}
