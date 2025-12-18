namespace Tarantino.WFI
{
    public interface IDialogNodeEditor
    {
        DialogNodeKind TargetKind { get; }
        DialogNode.Builder? EditedBuilder { get; }

        void LoadBuilder(DialogNode.Builder builder);
        void Show();
        void Hide();

        event Action<DialogNode.Builder>? ChangesMade;
    }
}
