using System.Collections.Immutable;
using Tarantino.IO;

namespace Tarantino.Model
{
    public class Dialog
    {
        public string Text { get; }
        public ImmutableArray<DialogResponse> Responses { get; }

        public Dialog(string text, ImmutableArray<DialogResponse> respones)
        {
            Text = text;
            Responses = respones;
        }

        public static Dialog Create(string text, params DialogResponse[] responses)
            => new Dialog(text, responses.ToImmutableArray());

        public string ToJson()
            => DialogSerializer.Serialize(this);

        public static Dialog FromJson(string json)
            => DialogSerializer.Deserialize(json);
    }

    public abstract class DialogResponse
    {
        public string Text { get; }
        public abstract DialogResponseKind Kind { get; }

        public DialogResponse(string text)
        {
            Text = text;
        }

        public static AnswerDialogResponse EndDialog(string text)
            => new AnswerDialogResponse(text, null);

        public static AnswerDialogResponse Answer(string text, string? answer = null)
            => new AnswerDialogResponse(text, answer);

        public static SubDialogResponse SubDialog(string text, Dialog dialog)
            => new SubDialogResponse(text, dialog);
    }

    public class AnswerDialogResponse : DialogResponse
    {
        public new string? Answer { get; }

        public override DialogResponseKind Kind => DialogResponseKind.Answer;

        public AnswerDialogResponse(string text, string? answer) : base(text)
        {
            Answer = answer;
        }        
    }

    public class SubDialogResponse : DialogResponse
    {
        public Dialog Dialog { get; }

        public override DialogResponseKind Kind => DialogResponseKind.SubDialog;

        public SubDialogResponse(string text, Dialog dialog) : base(text)
        {
            Dialog = dialog;
        }
    }
}
