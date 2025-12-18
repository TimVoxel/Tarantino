using System.Collections.Immutable;

namespace Tarantino
{
    public class AnswerDialogResponse : DialogResponse
    {
        public string? Answer { get; }

        public override DialogNodeKind Kind => DialogNodeKind.AnswerResponse;

        public AnswerDialogResponse(string text, ImmutableArray<DialogEvent> events, string? answer) : base(text, events)
        {
            Answer = answer;
        }

        public override ResponseBuilder ToBuilder()
            => new AnswerBuilder(Text, Events.Select(e => e.ToBuilder()).ToList(), Answer);

        public class AnswerBuilder : ResponseBuilder
        {
            public string? Answer { get; set; }
            public override DialogNodeKind Kind => DialogNodeKind.AnswerResponse;

            public AnswerBuilder(string text, List<DialogEvent.Builder> events, string? answer)
                : base(text, events)
            {
                Answer = answer;
            }

            public override ResponseBuilder ConvertToKind(DialogNodeKind kind)
            {
                return kind switch
                {
                    DialogNodeKind.AnswerResponse => this,
                    DialogNodeKind.RegistrySubDialogResponse => new RegistrySubDialogResponse.RegistrySubDialogBuilder(
                        Text,
                        Events,
                        string.Empty,
                        string.Empty),

                    DialogNodeKind.SubDialogResponse => new SubDialogResponse.SubDialogBuilder(
                        Text,
                        Events,
                        new Dialog.DialogBuilder()),
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
}
