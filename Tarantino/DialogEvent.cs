namespace Tarantino
{
    public abstract class DialogEvent
    {
        public abstract DialogEventKind Kind { get; }

        public class Builder
        {
            public DialogEventKind Kind { get; set; }
            public string Tag { get; set; }
            public string Value { get; set; }

            public Builder(DialogEventKind kind, string tag, string value)
            {
                Kind = kind;
                Tag = tag;
                Value = value;
            }

            public Builder() : this(DialogEventKind.ParameterChange, "sample_tag", "sample_value") { }

            public DialogEvent Build()
            {
                return Kind switch
                {
                    DialogEventKind.ParameterChange => new ParameterChangeEvent(Tag, Value),
                    DialogEventKind.Tag => new TagEvent(Tag),
                    _ => throw new Exception($"Unsupported DialogEventKind: {Kind}"),
                };
            }
        }

        public abstract Builder ToBuilder();
    }
}
