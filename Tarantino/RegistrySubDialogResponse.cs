using System.Collections.Immutable;

namespace Tarantino
{
    public class RegistrySubDialogResponse : DialogResponse
    {
        public string Registry { get; }
        public string DialogName { get; }

        public override DialogNodeKind Kind => DialogNodeKind.RegistrySubDialogResponse;

        public RegistrySubDialogResponse(string text, ImmutableArray<DialogEvent> events, string registry, string dialogName) : base(text, events)
        {
            Registry = registry;
            DialogName = dialogName;
        }

        public override ResponseBuilder ToBuilder()
            => new RegistrySubDialogBuilder(Text, Events.Select(e => e.ToBuilder()).ToList(), Registry, DialogName);

        public class RegistrySubDialogBuilder : ResponseBuilder
        {
            public string Registry { get; set; }
            public string DialogName { get; set; }
            public override DialogNodeKind Kind => DialogNodeKind.RegistrySubDialogResponse;

            public RegistrySubDialogBuilder(string text, List<DialogEvent.Builder> events, string registry, string dialogName)
                : base(text, events)
            {
                Registry = registry;
                DialogName = dialogName;
            }

            public override ResponseBuilder ConvertToKind(DialogNodeKind kind)
            {
                return kind switch
                {
                    DialogNodeKind.SubDialogResponse => new SubDialogResponse.SubDialogBuilder(Text, Events, new Dialog.DialogBuilder()),
                    DialogNodeKind.AnswerResponse => new AnswerDialogResponse.AnswerBuilder(Text, Events, null),
                    DialogNodeKind.RegistrySubDialogResponse => this,
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

                return new RegistrySubDialogResponse(Text, events.ToImmutable(), Registry, DialogName);
            }
        }
    }

}
