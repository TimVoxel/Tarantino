namespace Tarantino.WFI
{
    public interface IDialogNodeEditor
    {
        DialogNodeKind TargetKind { get; }
        void LoadBuilder(DialogNode.Builder builder);
        void Show();
        void Hide();

        event Action<DialogNode.Builder>? ChangesMade;
    }
}
