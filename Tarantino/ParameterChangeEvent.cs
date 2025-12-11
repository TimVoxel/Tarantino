namespace Tarantino
{
    public class ParameterChangeEvent : DialogEvent
    {
        public override DialogEventKind Kind => DialogEventKind.ParameterChange;
        public string Parameter { get; }
        public string Value { get; set; }

        public ParameterChangeEvent(string parameter, string value)
        {
            Parameter = parameter;
            Value = value;
        }

        public class ParameterChangeEventBuilder : Builder
        {
            public string Parameter { get; set; }
            public string Value { get; set; }
            public override DialogEventKind Kind => DialogEventKind.ParameterChange;

            public ParameterChangeEventBuilder(string parameter, string value)
            {
                Parameter = parameter;
                Value = value;
            }
            public override Builder ConvertToKind(DialogEventKind kind)
            {
                return kind switch
                {
                    DialogEventKind.ParameterChange => this,
                    DialogEventKind.Tag => new TagEvent.TagEventBuilder(Parameter),
                    _ => throw new Exception($"Cannot convert event with kind {Kind} to {kind}")
                };
            }

            public override DialogEvent Build()
                => new ParameterChangeEvent(Parameter, Value);
        }

        public override Builder ToBuilder()
            => new ParameterChangeEventBuilder(Parameter, Value);
    }
}
