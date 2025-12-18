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

        public DialogResponse.ResponseBuilder Response
        {
            get
            {
                var kind = (DialogNodeKind)(_typeComboBox.SelectedItem ?? throw new Exception("Unexpected selected item"));
                var text = _textTextBox.Text.Trim();

                return kind switch
                {
                    DialogNodeKind.AnswerResponse => new AnswerDialogResponse.AnswerBuilder(text, new List<DialogEvent.Builder>(), null),
                    DialogNodeKind.SubDialogResponse => new SubDialogResponse.SubDialogBuilder(text, new List<DialogEvent.Builder>(), new Dialog.DialogBuilder("Sample text dialog")),
                    _ => throw new Exception($"Unexpected DialogNodeKind {kind}")
                };
            }
        }
    }
}
