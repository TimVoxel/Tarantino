using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Tarantino.IO;
using Tarantino.Model;

public class DialogRegistry : IEnumerable<KeyValuePair<string, Dialog>>
{
    private readonly Dictionary<string, Dialog> _dialogs;

    public DialogRegistry(Dictionary<string, Dialog> dialogs)
    {
        _dialogs = dialogs;
    }

    public DialogRegistry() : this(new Dictionary<string, Dialog>())
    {
    }

    public IEnumerator<KeyValuePair<string, Dialog>> GetEnumerator()
        => _dialogs.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _dialogs.GetEnumerator();

    public Dialog this[string id]
        => _dialogs[id];
    public bool TryGetDialog(string id, [MaybeNullWhen(false)] out Dialog dialog)
        => _dialogs.TryGetValue(id, out dialog);
    
    public bool Contains(string id)
        => _dialogs.ContainsKey(id);
    public void Register(string key, Dialog dialog)
        => _dialogs.Add(key, dialog);
    
    public int Count => _dialogs.Count;

    public void Save(string dirPath, string fileExtension = "json")
    {
        foreach (var dialog in _dialogs)
        {
            var filePath = Path.Combine(dirPath, $"{dialog.Key}.{fileExtension}");
            var serialized = dialog.Value.ToJson();
            File.WriteAllText(filePath, serialized);
        }
    }

    public static DialogRegistry CreateFromDirectory(string dirPath, string searchPattern = "*.json")
    {
        var dialogFiles = Directory.GetFiles(dirPath, "*.json", SearchOption.AllDirectories);
        var dialogs = new Dictionary<string, Dialog>();

        foreach (var filePath in dialogFiles)
        {
            var json = File.ReadAllText(filePath);

            try
            {
                var dialog = DialogSerializer.Deserialize(json);
                var dialogId = Path.GetFileNameWithoutExtension(filePath);
                dialogs.Add(dialogId, dialog);
            }
            catch
            {
            }
        }

        return new DialogRegistry(dialogs);
    }
}
