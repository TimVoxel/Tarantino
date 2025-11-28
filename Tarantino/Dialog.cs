using System.Collections.Immutable;
using System.Security.Cryptography.X509Certificates;
using Tarantino.IO;

namespace Tarantino
{
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
            public abstract DialogNodeKind Kind { get; }
            public abstract Builder ConvertToKind(DialogNodeKind kind);

            public Builder(string text)
            {
                Text = text;
            }

            public Builder() : this(string.Empty)
            {
            }

            public abstract DialogNode Build();
        }

    }

    public class Dialog : DialogNode
    {
        public ImmutableArray<DialogResponse> Responses { get; }

        public override DialogNodeKind Kind => DialogNodeKind.Dialog;

        public Dialog(string text, ImmutableArray<DialogResponse> respones) : base(text)
        {
            Responses = respones;
        }

        public static Dialog Create(string text, params DialogResponse[] responses)
            => new Dialog(text, responses.ToImmutableArray());

        public string ToJson()
            => DialogSerializer.Serialize(this);

        public static Dialog FromJson(string json)
            => DialogSerializer.Deserialize(json);

        public DialogBuilder ToBuilder()
            => new DialogBuilder(Text, Responses.Select(r => r.ToBuilder()).ToList());

        public class DialogBuilder : Builder
        {
            public List<Builder> Responses { get; }
            public override DialogNodeKind Kind => DialogNodeKind.Dialog;

            public DialogBuilder(string text, List<Builder> responses)
            {
                Text = text;
                Responses = responses;
            }

            public DialogBuilder()
            {
                Text = string.Empty;
                Responses = new List<Builder>();
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

                foreach (var responseBuilder in Responses)
                {
                    responsesBuilder.Add((DialogResponse) responseBuilder.Build());
                }

                return new Dialog(Text, responsesBuilder.ToImmutable());
            }
        }
    }

    public abstract class DialogResponse : DialogNode
    {
        public DialogResponse(string text) : base(text)
        {
        }

        public static AnswerDialogResponse EndDialog(string text)
            => new AnswerDialogResponse(text, null);

        public static AnswerDialogResponse Answer(string text, string? answer = null)
            => new AnswerDialogResponse(text, answer);

        public static SubDialogResponse SubDialog(string text, Dialog dialog)
            => new SubDialogResponse(text, dialog);

        public abstract Builder ToBuilder();
    }

    public class AnswerDialogResponse : DialogResponse
    {
        public new string? Answer { get; }

        public override DialogNodeKind Kind => DialogNodeKind.AnswerResponse;

        public AnswerDialogResponse(string text, string? answer) : base(text)
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
                    DialogNodeKind.SubDialogResponse => new SubDialogResponse.SubDialogBuilder(Text, new Dialog.DialogBuilder("Sample dialog text", new List<Builder>())),
                    _ => throw new Exception($"Cannot convert node with kind {Kind} to {kind}")
                };
            }

            public override DialogResponse Build()
                => new AnswerDialogResponse(Text, Answer);
        }
    }

    public class SubDialogResponse : DialogResponse
    {
        public Dialog Dialog { get; }

        public override DialogNodeKind Kind => DialogNodeKind.SubDialogResponse;

        public SubDialogResponse(string text, Dialog dialog) : base(text)
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
                => new SubDialogResponse(Text, Dialog.Build());
        }
    }
}
