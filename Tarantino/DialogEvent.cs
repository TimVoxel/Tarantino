namespace Tarantino
{
    public abstract class DialogEvent
    {
        public abstract DialogEventKind Kind { get; }

        public abstract class Builder
        {
            public abstract DialogEventKind Kind { get; }
            public abstract Builder ConvertToKind(DialogEventKind kind);
            public abstract DialogEvent Build();
        }

        public abstract Builder ToBuilder();
    }
}
