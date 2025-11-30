using Tarantino.WFI.Forms;

namespace Tarantino.WFI
{
    public partial class DialogEditor : UserControl, IDialogNodeEditor
    {
        private Dialog.DialogBuilder? _loadedBuilder;
        public DialogNodeKind TargetKind => DialogNodeKind.Dialog;

        public event Action<DialogNode.Builder>? ChangesMade;
        public event Action<DialogNode.Builder>? ResponseAdded;

        public DialogEditor()
        {
            InitializeComponent();
        }

        public void LoadBuilder(DialogNode.Builder builder)
        {
            if (builder is not Dialog.DialogBuilder dialogBuilder)
            {
                throw new InvalidOperationException("Failed to convert builder to AnswerDialogResponse.AnswerBuilder.");
            }

            _loadedBuilder = dialogBuilder;
            _textTextBox.Text = dialogBuilder.Text;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (_loadedBuilder != null)
            {
                var startPos = _textTextBox.SelectionStart;
                _loadedBuilder.Text = _textTextBox.Text;
                ChangesMade?.Invoke(_loadedBuilder);

                _textTextBox.SelectionStart = startPos;
            }
        }

        private void OnAddResponseClicked(object sender, EventArgs e)
        {
            if (_loadedBuilder != null)
            {
                var responseCreator = new ResponseCreator();

                var result = responseCreator.ShowDialog();

                if (result == DialogResult.OK)
                {
                    var responseBuilder = responseCreator.Response;
                    _loadedBuilder.Responses.Add(responseBuilder);
                    ResponseAdded?.Invoke(responseBuilder);
                }
            }
        }
    }
}
