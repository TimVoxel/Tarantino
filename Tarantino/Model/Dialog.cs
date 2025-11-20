using System.Collections.Immutable;

namespace Tarantino.Model
{
    public class Dialog
    {
        public string Text { get; }
        public ImmutableArray<DialogResponse> Responses { get; init; }

        private Dialog(string text)
        {
            Text = text;
        }

        public static Dialog Create(string text, params DialogResponse[] responses)
        {
            return new Dialog(text)
            {
                Responses = responses.ToImmutableArray()
            };
        }
    }

    public abstract class DialogResponse
    {
        public static TextDialogResponse Text(string text)
        {
            return new TextDialogResponse(text);
        }

        public static SubDialogResponse SubDialog(Dialog dialog)
        {
            return new SubDialogResponse(dialog);
        }
    }

    public class TextDialogResponse : DialogResponse
    {
        public new string Text { get; }

        public TextDialogResponse(string text)
        {
            Text = text;
        }
    }

    public class SubDialogResponse : DialogResponse
    {
        public Dialog Dialog { get; }

        public SubDialogResponse(Dialog dialog)
        {
            Dialog = dialog;
        }
    }
}
