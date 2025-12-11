using System.Collections.Immutable;

namespace Tarantino
{
    public class SubDialogResponse : DialogResponse
    {
        public Dialog Dialog { get; }

        public override DialogNodeKind Kind => DialogNodeKind.SubDialogResponse;

        public SubDialogResponse(string text, ImmutableArray<DialogEvent> events, Dialog dialog) : base(text, events)
        {
            Dialog = dialog;
        }

        public override ResponseBuilder ToBuilder()
            => new SubDialogBuilder(Text, Events.Select(e => e.ToBuilder()).ToList(), Dialog.ToBuilder());

        public class SubDialogBuilder : ResponseBuilder
        {
            public Dialog.DialogBuilder Dialog { get; set; }
            public override DialogNodeKind Kind => DialogNodeKind.SubDialogResponse;

            public SubDialogBuilder(string text, List<DialogEvent.Builder> events, Dialog.DialogBuilder dialog)
                : base(text, events)
            {
                Dialog = dialog;
            }

            public override ResponseBuilder ConvertToKind(DialogNodeKind kind)
            {
                return kind switch
                {
                    DialogNodeKind.SubDialogResponse => this,
                    DialogNodeKind.AnswerResponse => new AnswerDialogResponse.AnswerBuilder(Text, Events, null),
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
