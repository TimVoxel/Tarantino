namespace Tarantino.WFI
{
    public class DialogTreeNode : TreeNode
    {
        public DialogNode.Builder? Builder => Tag as DialogNode.Builder;

        public DialogTreeNode() : base()
        {
        }

        public DialogTreeNode(DialogNode.Builder node) : base()
            => RefreshTag(node);

        public void RefreshTag(DialogNode.Builder builder)
        {
            Tag = builder;

            Text = builder switch
            {
                Dialog.DialogBuilder dialogBuilder => dialogBuilder.Text.First().Text, // TODO: replace with component support
                SubDialogResponse.SubDialogBuilder subDialogBuilder => $"{subDialogBuilder.Kind}: {subDialogBuilder.Text}",
                AnswerDialogResponse.AnswerBuilder answerBuilder => $"{answerBuilder.Kind}: {answerBuilder.Text}",
                _ => Text
            };
        }   
    }
}