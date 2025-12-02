using System.Collections.Immutable;
using Tarantino.IO;

namespace Tarantino
{
    public enum DialogEventKind
    {
        ParameterChange,
        Tag
    }

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

    public abstract class DialogNode
    {
        public abstract DialogNodeKind Kind { get; }
        public string Text { get; }

        public DialogNode(string text)
        {
            Text = text;
        }

        public abstract class Builder
        { 
            public string Text { get; set; }
            public List<DialogEvent.Builder> Events { get; }

            public abstract DialogNodeKind Kind { get; }
            public abstract Builder ConvertToKind(DialogNodeKind kind);

            public Builder(string text, List<DialogEvent.Builder> events)
            {
                Text = text;
                Events = events;
            }

            public Builder(string text) : this(text, new List<DialogEvent.Builder>())
            {
            }

            public Builder() : this(string.Empty, new List<DialogEvent.Builder>())
            {
            }

            public abstract DialogNode Build();
        }

    }

    public class Dialog : DialogNode
    {
        public ImmutableArray<DialogResponse> Responses { get; }
        public ImmutableArray<DialogEvent> Events { get; }
        public override DialogNodeKind Kind => DialogNodeKind.Dialog;

        public Dialog(string text, 
                      ImmutableArray<DialogResponse> respones,
                      ImmutableArray<DialogEvent> events) : base(text)
        {
            Responses = respones;
            Events = events;
        }

        public string ToJson()
            => DialogSerializer.Serialize(this);

        public static Dialog FromJson(string json)
            => DialogSerializer.Deserialize(json);

        public DialogBuilder ToBuilder()
            => new DialogBuilder(Text, 
                    Responses.Select(r => r.ToBuilder()).ToList(), 
                    Events.Select(e => e.ToBuilder()).ToList());

        public class DialogBuilder : Builder
        {
            public List<Builder> Responses { get; }

            public override DialogNodeKind Kind => DialogNodeKind.Dialog;

            public DialogBuilder(string text, List<Builder> responses, List<DialogEvent.Builder> events) : base(text, events)
            {
                Responses = responses;
            }

            public DialogBuilder(string text) : this(text, new List<Builder>(), new List<DialogEvent.Builder>())
            {
            }

            public DialogBuilder() : this(string.Empty, new List<Builder>(), new List<DialogEvent.Builder>())
            {
            }

            public DialogBuilder SetText(string text)
            {
                Text = text;
                return this;
            }

            public DialogBuilder AddResponse(DialogResponse response)
            {
                Responses.Add(response.ToBuilder());
                return this;
            }

            public override Builder ConvertToKind(DialogNodeKind kind)
            {
                return kind switch
                {
                    DialogNodeKind.Dialog => this,
                    _ => throw new Exception($"Cannot convert node with kind {Kind} to {kind}")
                };
            }

            public override Dialog Build()
            {
                var responsesBuilder = ImmutableArray.CreateBuilder<DialogResponse>(Responses.Count);
                var eventsBuilder = ImmutableArray.CreateBuilder<DialogEvent>(Events.Count); 

                foreach (var responseBuilder in Responses)
                {
                    responsesBuilder.Add((DialogResponse) responseBuilder.Build());
                }

                foreach (var eventBuilder in Events)
                {
                    eventsBuilder.Add(eventBuilder.Build());
                }

                return new Dialog(Text, responsesBuilder.ToImmutable(), eventsBuilder.ToImmutable());
            }
        }
    }

    public abstract class DialogResponse : DialogNode
    {
        public ImmutableArray<DialogEvent> Events { get; }

        public DialogResponse(string text, ImmutableArray<DialogEvent> events) : base(text)
        {
            Events = events;
        }

        public abstract Builder ToBuilder();
    }

    public class AnswerDialogResponse : DialogResponse
    {
        public string? Answer { get; }

        public override DialogNodeKind Kind => DialogNodeKind.AnswerResponse;

        public AnswerDialogResponse(string text, ImmutableArray<DialogEvent> events, string? answer) : base(text, events)
        {
            Answer = answer;
        }

        public override Builder ToBuilder()
            => new AnswerBuilder(Text, Answer);

        public class AnswerBuilder : Builder
        {
            public string? Answer { get; set; }
            public override DialogNodeKind Kind => DialogNodeKind.AnswerResponse;

            public AnswerBuilder(string text, string? answer)
                : base(text)
            {
                Answer = answer;
            }

            public override Builder ConvertToKind(DialogNodeKind kind)
            {
                return kind switch
                {
                    DialogNodeKind.AnswerResponse => this,
                    DialogNodeKind.SubDialogResponse => new SubDialogResponse.SubDialogBuilder(Text, new Dialog.DialogBuilder("Sample dialog text", new List<Builder>(), new List<DialogEvent.Builder>())),
                    _ => throw new Exception($"Cannot convert node with kind {Kind} to {kind}")
                };
            }

            public override DialogResponse Build()
            {
                var events = ImmutableArray.CreateBuilder<DialogEvent>();

                foreach (var e in Events)
                {
                    events.Add(e.Build());
                }

                return new AnswerDialogResponse(Text, events.ToImmutable(), Answer);
            }   
        }
    }

    public class SubDialogResponse : DialogResponse
    {
        public Dialog Dialog { get; }

        public override DialogNodeKind Kind => DialogNodeKind.SubDialogResponse;

        public SubDialogResponse(string text, ImmutableArray<DialogEvent> events, Dialog dialog) : base(text, events)
        {
            Dialog = dialog;
        }

        public override Builder ToBuilder()
            => new SubDialogBuilder(Text, Dialog.ToBuilder());

        public class SubDialogBuilder : Builder
        {
            public Dialog.DialogBuilder Dialog { get; set; }
            public override DialogNodeKind Kind => DialogNodeKind.SubDialogResponse;

            public SubDialogBuilder(string text, Dialog.DialogBuilder dialog)
                : base(text)
            {
                Dialog = dialog;
            }

            public override Builder ConvertToKind(DialogNodeKind kind)
            {
                return kind switch
                {
                    DialogNodeKind.SubDialogResponse => this,
                    DialogNodeKind.AnswerResponse => new AnswerDialogResponse.AnswerBuilder(Text, null),
                    _ => throw new Exception($"Cannot convert node with kind {Kind} to {kind}")
                };
            }

            public override DialogResponse Build()
            {
                var events = ImmutableArray.CreateBuilder<DialogEvent>();

                foreach (var e in Events)
                {
                    events.Add(e.Build());
                }

                return new SubDialogResponse(Text, events.ToImmutable(), Dialog.Build());
            }
        }
    }
}
