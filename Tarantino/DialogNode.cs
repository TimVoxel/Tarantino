using System.Collections.Immutable;

namespace Tarantino
{
    public abstract class DialogNode
    {
        public abstract DialogNodeKind Kind { get; }
        public ImmutableArray<DialogEvent> Events { get; }

        public DialogNode(ImmutableArray<DialogEvent> events)
        {
            Events = events;
        }

        public abstract class Builder
        { 
            public List<DialogEvent.Builder> Events { get; }
            public abstract DialogNodeKind Kind { get; }

            public Builder(List<DialogEvent.Builder> events)
            {
                Events = events;
            }

            public Builder() : this(new List<DialogEvent.Builder>())
            {
            }

            public abstract DialogNode Build();
        }
    }
}
