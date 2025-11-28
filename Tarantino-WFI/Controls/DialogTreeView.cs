using System.Diagnostics;

namespace Tarantino.WFI
{
    public partial class DialogTreeView : UserControl
    {
        private Dialog.DialogBuilder? _loadedBuilder;
        private DialogTreeNode? _dragged;

        private Dictionary<DialogTreeNode, Dialog.DialogBuilder> _nodeToStashedSubDialog;

        public event Action<DialogTreeNode?, DialogNode.Builder?>? NodeSelected;

        public bool HasLoadedDialog => _loadedBuilder != null;
        public DialogTreeNode Root => (DialogTreeNode) _handle.Nodes[0];

        public DialogTreeNode? SelectedNode
        {
            get => _handle.SelectedNode as DialogTreeNode;
            set => _handle.SelectedNode = value;
        }

        public DialogTreeView()
        {
            InitializeComponent();
            _nodeToStashedSubDialog = new Dictionary<DialogTreeNode, Dialog.DialogBuilder>();
        }

        public Dialog.DialogBuilder LoadDialog(Dialog dialog)
        {
            var builder = dialog.ToBuilder();
            _loadedBuilder = builder;
            _handle.Nodes.Clear();
            _handle.Nodes.Add(BuildDialogNode(_loadedBuilder));
            _handle.ExpandAll();

            return builder;
        }

        public void AddDialogResponse(DialogTreeNode node, DialogResponse.Builder builder)
        {            
            var responseNode = BuildResponseNode(builder);
            node.Nodes.Add(responseNode);
        }

        private DialogTreeNode BuildDialogNode(Dialog.DialogBuilder dialog)
        {
            var dialogNode = new DialogTreeNode(dialog);
            
            foreach (var response in dialog.Responses)
            {
                dialogNode.Nodes.Add(BuildResponseNode(response));
            }

            return dialogNode;
        }

        private DialogTreeNode BuildResponseNode(DialogNode.Builder response)
        {
            var responseNode = new DialogTreeNode(response);
            
            if (response is SubDialogResponse.SubDialogBuilder sub)
            {
                responseNode.Nodes.Add(BuildDialogNode(sub.Dialog));
            }

            return responseNode;
        }
        
        private void OnItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is DialogTreeNode dialogTreeNode)
            {
                DoDragDrop(dialogTreeNode, DragDropEffects.Move);
            }   
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            var point = _handle.PointToClient(new Point(e.X, e.Y));
            var node = _handle.GetNodeAt(point);
            _handle.SelectedNode = node;
            e.Effect = DragDropEffects.Move;
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            if (_dragged == null)
            {
                return;
            }

            var point = _handle.PointToClient(new Point(e.X, e.Y));
            var target = _handle.GetNodeAt(point) as DialogTreeNode;
            var draggedParent = _dragged.Parent;

            if (target == null || target == _dragged || draggedParent == null)
            {
                return;
            }

            _dragged.Remove();
            target.Parent.Nodes.Insert(target.Index + 1, _dragged);

            var draggedBuilder = (DialogNode.Builder) _dragged.Tag!;

            var draggedList = GetBuilderListForParent(draggedParent);
            var targetList = GetBuilderListForParent(target.Parent);

            var targetBuilder = (DialogNode.Builder)target.Tag!;

            if (draggedList != targetList)
            {
                return;
            }

            // Remove and reinsert in same builder list
            draggedList.Remove(draggedBuilder);
            var insertIndex = targetList.IndexOf(targetBuilder) + 1;
            draggedList.Insert(insertIndex, draggedBuilder);

            _handle.SelectedNode = _dragged;
            _dragged = null;
        }

        private void OnTreeKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var confirmed = e.Shift || MessageBox.Show("Are you sure you want to delete this node?",
                                                           "Confirm Delete",
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Warning) == DialogResult.Yes;

                if (!confirmed)
                {
                    return;
                }

                var node = _handle.SelectedNode as DialogTreeNode;
                if (node != null)
                {
                    DeleteNode(node);
                    e.Handled = true;
                }
            }
        }

        private void DeleteNode(DialogTreeNode node)
        {
            if (node.Parent == null)
            {
                return;
            }

            var builder = node.Builder;
            Debug.Assert(builder != null);
            var parent = node.Parent as DialogTreeNode;
            var parentBuilders = GetBuilderListForParent(parent);
            parentBuilders.Remove(builder);

            _nodeToStashedSubDialog.Remove(node);
            node.Remove();
        }

        private List<DialogNode.Builder> GetBuilderListForParent(TreeNode? parent)
        {
            if (parent is DialogTreeNode dlg)
            {
                var builder = dlg.Builder;

                return builder switch
                {
                    Dialog.DialogBuilder dialog => dialog.Responses,
                    SubDialogResponse.SubDialogBuilder sub => sub.Dialog.Responses,
                    _ => throw new InvalidOperationException("Unsupported builder parent.")
                };
            }
            else
            {
                if (_loadedBuilder != null)
                    return _loadedBuilder.Responses;

                throw new InvalidOperationException("No dialog loaded.");
            }
        }

        private void OnNodeSelected(object sender, TreeViewEventArgs e)
            => NodeSelected?.Invoke(e.Node as DialogTreeNode, e.Node?.Tag as DialogNode.Builder);

        internal Dialog BuildDialog()
            => _loadedBuilder?.Build() ?? throw new InvalidOperationException("No dialog loaded");

        internal void RefreshNode(DialogTreeNode node, DialogNode.Builder builder)
        {
            var oldBuilder = node.Builder;
            var newKind = builder.Kind;

            if (oldBuilder?.Kind == newKind)
            {
                node.RefreshTag(builder);
                return;
            }
            
            switch (newKind)
            {
                case DialogNodeKind.AnswerResponse:

                    if (oldBuilder is SubDialogResponse.SubDialogBuilder sub)
                    {
                        _nodeToStashedSubDialog.Add(node, sub.Dialog);
                    }
                    node.Nodes.Clear();
                    node.RefreshTag(builder);
                    break;

                case DialogNodeKind.SubDialogResponse:
                    var subDialogBuilder = (SubDialogResponse.SubDialogBuilder)builder;

                    if (_nodeToStashedSubDialog.TryGetValue(node, out var stashed))
                    {
                        subDialogBuilder.Dialog = stashed;
                        _nodeToStashedSubDialog.Remove(node);
                    }

                    var newSubDialog = BuildDialogNode(subDialogBuilder.Dialog);
                    node.Nodes.Add(newSubDialog);
                    node.RefreshTag(builder);
                    break;

                default:
                    throw new Exception($"Kind {newKind} cannot have their kind changed, therefore the node is not rebuildable");
            }
        }
    }
}