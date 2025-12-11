using System.Collections.Immutable;

namespace Tarantino
{
    public abstract class DialogResponse : DialogNode
    {
        public string Text { get; }
        public ImmutableArray<DialogEvent> Events { get; }

        public DialogResponse(string text, ImmutableArray<DialogEvent> events) : base()
        {
            Text = text;
            Events = events;
        }

        public abstract class ResponseBuilder : Builder
        {
            public string Text { get; set; }

            public ResponseBuilder(string text, List<DialogEvent.Builder> events)
                : base(events)
            {
                Text = text;
            }

            public abstract ResponseBuilder ConvertToKind(DialogNodeKind kind);
        }

        public abstract ResponseBuilder ToBuilder();
    }
}
