namespace Tarantino
{
    public abstract class DialogNode
    {
        public abstract DialogNodeKind Kind { get; }

        public DialogNode()
        {
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
