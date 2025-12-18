using System.Collections.Immutable;
using Tarantino.IO;

namespace Tarantino
{
    public class Dialog : DialogNode
    {
        public ImmutableArray<TextComponent> Text { get; }
        public ImmutableArray<DialogResponse> Responses { get; }
        public override DialogNodeKind Kind => DialogNodeKind.Dialog;

        public Dialog(ImmutableArray<TextComponent> text, 
                      ImmutableArray<DialogResponse> respones,
                      ImmutableArray<DialogEvent> events) : base(events)
        {
            Text = text;
            Responses = respones;
        }

        public string ToJson()
            => DialogSerializer.Serialize(this);

        public static Dialog FromJson(string json)
            => DialogSerializer.Deserialize(json);

        public DialogBuilder ToBuilder()
            => new DialogBuilder(
                    Text.Select(t => t.ToBuilder()).ToList(), 
                    Responses.Select(r => r.ToBuilder()).ToList(), 
                    Events.Select(e => e.ToBuilder()).ToList());

        public class DialogBuilder : Builder
        {
            public List<TextComponent.Builder> Text { get; }
            public List<DialogResponse.ResponseBuilder> Responses { get; }

            public override DialogNodeKind Kind => DialogNodeKind.Dialog;

            public DialogBuilder(List<TextComponent.Builder> text,
                                 List<DialogResponse.ResponseBuilder> responses,
                                 List<DialogEvent.Builder> events) : base(events)
            {
                Text = text;
                Responses = responses;
            }

            public DialogBuilder(List<TextComponent.Builder> text) : this(text, new List<DialogResponse.ResponseBuilder>(), new List<DialogEvent.Builder>())
            {
            }

            public DialogBuilder(string text) : this(new List<TextComponent.Builder>()
            {
                new TextComponent.Builder(text, TextComponentKind.PlainText)
            }) { }

            public DialogBuilder() : this(
                new List<TextComponent.Builder>(),
                new List<DialogResponse.ResponseBuilder>(), 
                new List<DialogEvent.Builder>())
            {
            }

            /*
            public DialogBuilder SetText(string text)
            {
                Text = text;
                return this;
            }
            */

            public DialogBuilder AddResponse(DialogResponse response)
            {
                Responses.Add(response.ToBuilder());
                return this;
            }

            public override Dialog Build()
            {
                var textBuilder = ImmutableArray.CreateBuilder<TextComponent>();
                var responsesBuilder = ImmutableArray.CreateBuilder<DialogResponse>(Responses.Count);
                var eventsBuilder = ImmutableArray.CreateBuilder<DialogEvent>(Events.Count); 

                foreach (var textComponentBuilder in Text)
                {
                    textBuilder.Add(textComponentBuilder.Build());
                }

                foreach (var responseBuilder in Responses)
                {
                    responsesBuilder.Add((DialogResponse) responseBuilder.Build());
                }

                foreach (var eventBuilder in Events)
                {
                    eventsBuilder.Add(eventBuilder.Build());
                }

                return new Dialog(textBuilder.ToImmutable(),
                                  responsesBuilder.ToImmutable(),
                                  eventsBuilder.ToImmutable());
            }
        }
    }
}
