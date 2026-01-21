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

        public override Builder ToBuilder()
            => new Builder(DialogEventKind.Tag, Tag, string.Empty);
    }
}
