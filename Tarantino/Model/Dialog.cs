using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public abstract DialogResponseKind Kind { get; }

        public static TextDialogResponse Text(string text, string? answer = null)
            => new TextDialogResponse(text, answer);

        public static SubDialogResponse SubDialog(Dialog dialog)
            => new SubDialogResponse(dialog);
    }

    public class TextDialogResponse : DialogResponse
    {
        public new string Text { get; }
        public string? Answer { get; }

        public override DialogResponseKind Kind => DialogResponseKind.Text;

        public TextDialogResponse(string text, string? answer)
        {
            Text = text;
            Answer = answer;
        }        
    }

    public class SubDialogResponse : DialogResponse
    {
        public Dialog Dialog { get; }

        public override DialogResponseKind Kind => DialogResponseKind.SubDialog;

        public SubDialogResponse(Dialog dialog)
        {
            Dialog = dialog;
        }
    }
}
