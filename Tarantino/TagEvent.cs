namespace Tarantino
{
    public class TagEvent : DialogEvent
    {
        public override DialogEventKind Kind => DialogEventKind.Tag;
        public string Tag { get; }

        public TagEvent(string tag)
        {
            Tag = tag;
        }

        public class TagEventBuilder : Builder
        {
            public string Tag { get; set; }
            public override DialogEventKind Kind => DialogEventKind.Tag;


            public TagEventBuilder() : this(string.Empty) {}

            public TagEventBuilder(string tag)
            {
                Tag = tag;
            }

            public override Builder ConvertToKind(DialogEventKind kind)
            {
                return kind switch
                {
                    DialogEventKind.Tag => this,
                    DialogEventKind.ParameterChange => new ParameterChangeEvent.ParameterChangeEventBuilder(string.Empty, string.Empty),
                    _ => throw new Exception($"Cannot convert event with kind {Kind} to {kind}")
                };
            }

            public override DialogEvent Build()
                => new TagEvent(Tag);
        }

        public override Builder ToBuilder()
            => new TagEventBuilder(Tag);
    }
}
