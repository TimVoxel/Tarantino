namespace Tarantino
{
    public class TextComponent
    {
        public TextComponentKind Kind { get; }
        public string Text { get; }

        public TextComponent(string text, TextComponentKind kind)
        {
            Kind = kind;
            Text = text;
        }

        public class Builder
        {
            public TextComponentKind Kind { get; set; }
            public string Text { get; set; }

            public Builder(string text, TextComponentKind kind)
            {
                Kind = kind;
                Text = text;
            }

            public TextComponent Build()
                => new TextComponent(Text, Kind);
        }

        public Builder ToBuilder()
            => new Builder(Text, Kind);
    }
}
