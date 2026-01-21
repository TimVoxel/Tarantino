using System.ComponentModel;
using Tarantino.WFI.Forms;

namespace Tarantino.WFI
{
    public partial class DialogEditor : UserControl, IDialogNodeEditor
    {
        private Dialog.DialogBuilder? _loadedBuilder;
        public DialogNodeKind TargetKind => DialogNodeKind.Dialog;

        public event Action<DialogNode.Builder>? ChangesMade;
        public event Action<DialogNode.Builder>? ResponseAdded;

        public DialogNode.Builder? EditedBuilder => _loadedBuilder;

        public DialogEditor()
        {
            InitializeComponent();

            var source = new List<TextComponent.Builder>()
            {
                new TextComponent.Builder("Sample text", TextComponentKind.PlainText)
            };

            _textView.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (_textView.IsCurrentCellDirty)
                {
                    _textView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };

            _textView.DataSource = source;

            var comboColumn = new DataGridViewComboBoxColumn
            {
                Name = "Kind",
                HeaderText = "Kind",
                DataPropertyName = "Kind",
                DataSource = Enum.GetValues(typeof(TextComponentKind))
            };

            var index = _textView.Columns["Kind"].Index;
            _textView.Columns.RemoveAt(index);
            _textView.Columns.Insert(index, comboColumn);
        }

        private void OnTextComponentCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _loadedBuilder == null)
            {
                return;
            }

            var cell = _textView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var value = cell.Value;

            if (e.ColumnIndex == 1)
            {
                ChangesMade?.Invoke(_loadedBuilder);
            }
        }

        public void LoadBuilder(DialogNode.Builder builder)
        {
            if (builder is not Dialog.DialogBuilder dialogBuilder)
            {
                throw new InvalidOperationException("Failed to convert builder to AnswerDialogResponse.AnswerBuilder.");
            }

            _loadedBuilder = dialogBuilder;
            var dataSource = new BindingList<TextComponent.Builder>(_loadedBuilder.Text);
            dataSource.AllowNew = true;
            _textView.AllowUserToAddRows = true;
            _textView.AutoGenerateColumns = true;
            _textView.DataSource = dataSource;
            _eventEditor.Bind(builder.Events ?? new List<DialogEvent.Builder>());
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
