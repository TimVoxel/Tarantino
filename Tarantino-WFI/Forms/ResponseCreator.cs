namespace Tarantino.WFI.Forms
{
    public partial class ResponseCreator : Form
    {
        public ResponseCreator()
        {
            InitializeComponent();
            _typeComboBox.DataSource = DialogFacts.ResponseDialogKinds.ToArray();
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OnDoneButtonClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public DialogNode.Builder Response
        {
            get
            {
                var kind = (DialogNodeKind)(_typeComboBox.SelectedItem ?? throw new Exception("Unexpected selected item"));
                var text = _textTextBox.Text.Trim();

                return kind switch
                {
                    DialogNodeKind.AnswerResponse => new AnswerDialogResponse.AnswerBuilder(text, null),
                    DialogNodeKind.SubDialogResponse => new SubDialogResponse.SubDialogBuilder(text, new Dialog.DialogBuilder("Sample dialog text", new List<DialogNode.Builder>())),
                    _ => throw new Exception($"Unexpected DialogNodeKind {kind}")
                };
            }
        }
    }
}
