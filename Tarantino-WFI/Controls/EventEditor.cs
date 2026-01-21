using System.ComponentModel;

namespace Tarantino.WFI
{
    public partial class EventEditor : UserControl
    {
        private class UniversalEventBuilder
        {
            private DialogEventKind _kind;
            public DialogEventKind Kind {
                get => _kind;
                set
                {
                    _kind = value;
                    PropertyChanged?.Invoke();
                }
            }

            private string _tag;
            public string Tag
            {
                get => _tag;
                set
                {
                    _tag = value;
                    PropertyChanged?.Invoke();
                }
            }

            private string? _value;
            public string? Value
            {
                get => _value;
                set
                {
                    if (Kind != DialogEventKind.ParameterChange)
                    {
                        _value = null;
                        PropertyChanged?.Invoke();
                    }

                    _value = value;
                    PropertyChanged?.Invoke();
                }
            }

            public event Action? PropertyChanged;

            public UniversalEventBuilder(DialogEventKind kind, string tag, string? value)
            {
                Kind = kind;
                _tag = tag;
                _value = value;
            }

            public UniversalEventBuilder() : this(DialogEventKind.Tag, "sample_tag", "")
            {}
        }

        private List<DialogEvent.Builder>? _loadedEvents;

        public EventEditor()
        {
            InitializeComponent();
            //_typeComboBox.DataSource = DialogFacts.ResponseDialogKinds.ToArray();
            
            _handle.DataSource = new List<UniversalEventBuilder>();

            {
                var comboColumn = new DataGridViewComboBoxColumn
                {
                    Name = "Kind",
                    HeaderText = "Kind",
                    DataPropertyName = "Kind",
                    DataSource = Enum.GetValues(typeof(DialogEventKind))
                };

                var index = _handle.Columns["Kind"].Index;
                _handle.Columns.RemoveAt(index);
                _handle.Columns.Insert(index, comboColumn);
            }
        }

        public void Bind(List<DialogEvent.Builder> dialogBuilder)
        {
            _loadedEvents = dialogBuilder;
            
            /*
            var sourceList = dialogBuilder.Select(builder => new UniversalEventBuilder(
                builder.Kind,
                builder switch
                {
                    TagEvent.TagEventBuilder tagBuilder => tagBuilder.Tag,
                    ParameterChangeEvent.ParameterChangeEventBuilder parameterBuilder => parameterBuilder.Parameter,
                    _ => throw new Exception($"Unexpected builder kind {builder.Kind}")
                },
                builder is ParameterChangeEvent.ParameterChangeEventBuilder parameterChangeBuilder
                    ? parameterChangeBuilder.Value
                    : null
            )).ToList();
            
            for (var i = 0; i < sourceList.Count; i++)
            {
                var item = sourceList[i];
                item.PropertyChanged += () => OnEventListChanged(this, new ListChangedEventArgs(ListChangedType.ItemChanged, i));
            }*/

            var bindingList = new BindingList<DialogEvent.Builder>(dialogBuilder);
            //bindingList.ListChanged += OnEventListChanged;
            _handle.AllowUserToAddRows = true;
            _handle.DataSource = bindingList;
        }

        /*
        public IEnumerable<DialogEvent.Builder> GetEvents()
        {
            return (List<DialogEvent.Builder>) _handle.DataSource;
            
            var bindings = (BindingList<UniversalEventBuilder>) _handle.DataSource;

            foreach (var binding in bindings)
            {
                yield return binding.Kind switch
                {
                    DialogEventKind.Tag => new TagEvent.TagEventBuilder(binding.Tag),
                    DialogEventKind.ParameterChange => new ParameterChangeEvent.ParameterChangeEventBuilder(binding.Tag, binding.Value ?? string.Empty),
                    _ => throw new Exception($"Unexpected event kind {binding.Kind}")
                };
            }
        }*/
        
        /*
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
                var startPos = _textTextBox.SelectionStart;
                _loadedBuilder.Text = _textTextBox.Text;
                ChangesMade?.Invoke(_loadedBuilder);

                _textTextBox.SelectionStart = startPos;
            }
        }

        private void OnAnswerChanged(object sender, EventArgs e)
        {
            if (_loadedBuilder != null)
            {   
                var startPos = _answerTextBox.SelectionStart;
                var text = _answerTextBox.Text;

                _loadedBuilder.Answer = !string.IsNullOrWhiteSpace(text)
                    ? text
                    : null;

                ChangesMade?.Invoke(_loadedBuilder);
                _answerTextBox.SelectionStart = startPos;
            }
        }*/
    }
}
