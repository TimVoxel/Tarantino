using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Tarantino.IO;
using Tarantino.Model;

namespace Tarantino.REPL
{
    internal class TarantinoREPL
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
        private sealed class MetaCommandAttribute : Attribute
        {
            public string Name { get; }
            public string Description { get; }

            public MetaCommandAttribute(string name, string description)
            {
                Name = name;
                Description = description;
            }
        }

        private sealed class MetaCommand
        {
            public string Name { get; }
            public string Description { get; }
            public MethodInfo Method { get; }

            public MetaCommand(string name, string description, MethodInfo method)
            {
                Name = name;
                Description = description;
                Method = method;
            }
        }

        private readonly List<MetaCommand> _metaCommands = new List<MetaCommand>();
        private readonly Dictionary<string, DialogRegistry> _loadedRegistries = new Dictionary<string, DialogRegistry>();
        private string _contextDir = AppContext.BaseDirectory;

        public TarantinoREPL()
        {
            LoadMetaCommands();
        }

        private void LoadMetaCommands()
        {
            foreach (var method in GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy))
            {
                var attribute = (MetaCommandAttribute?)method.GetCustomAttribute(typeof(MetaCommandAttribute));
                if (attribute == null)
                    continue;

                var command = new MetaCommand(attribute.Name, attribute.Description, method);
                _metaCommands.Add(command);
            }
        }

        public void Run()
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == null)
                {
                    continue;
                }

                var args = new List<string>();
                var inQuotes = false;
                var position = 0;
                var stringBuilder = new StringBuilder();

                while (position < input.Length)
                {
                    var c = input[position];
                    var l = position + 1 >= input.Length ? '\0' : input[position + 1];

                    if (char.IsWhiteSpace(c))
                    {
                        if (!inQuotes)
                        {
                            CommitPendingArgument();
                        }
                        else
                        {
                            stringBuilder.Append(c);
                        }
                    }
                    else if (c == '\"')
                    {
                        if (!inQuotes)
                        {
                            inQuotes = true;
                        }
                        else if (l == '\"')
                        {
                            stringBuilder.Append(c);
                            position++;
                        }
                        else
                        {
                            inQuotes = false;
                        }
                    }
                    else
                    {
                        stringBuilder.Append(c);
                    }

                    position++;
                }

                CommitPendingArgument();

                void CommitPendingArgument()
                {
                    var arg = stringBuilder.ToString();

                    if (!string.IsNullOrWhiteSpace(arg))
                    {
                        args.Add(arg);
                    }

                    stringBuilder.Clear();
                }

                var commandName = args.FirstOrDefault();
                if (args.Count > 0)
                {
                    args.RemoveAt(0);
                }

                var command = _metaCommands.SingleOrDefault(mc => mc.Name == commandName);

                if (command == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"error: Invalid command \"{commandName}\"");
                    Console.ResetColor();
                    continue;
                }

                var parameters = command.Method.GetParameters();

                if (args.Count != parameters.Length)
                {
                    var parameterNames = string.Join(", ", parameters.Select(p => $"<{p.Name}>"));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"error: Invalid number of arguments");
                    Console.WriteLine($"usage: {command.Name} {parameterNames}");
                    Console.ResetColor();
                    continue;
                }

                command.Method.Invoke(this, args.ToArray());
            }
        }

        [MetaCommand("help", "Shows the list of all commands")]
        private void EvaluateHelp()
        {
            var maxLength = _metaCommands.Max(mc => mc.Name.Length);

            foreach (var command in _metaCommands.OrderBy(mc => mc.Name))
            {
                var paddedName = command.Name.PadRight(maxLength);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(paddedName);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"  {command.Description}");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        [MetaCommand("cd", "Change context directory to specified")]
        private void EvaluateChangeDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"error: Directory not found: {path}");
                Console.ResetColor();
                return;
            }
            _contextDir = Path.GetFullPath(path);
            Console.WriteLine($"Context directory changed to: {_contextDir}");
        }

        [MetaCommand("pprint", "Loads, deserializes and pretty prints the dialog structure at the specified path")]
        private void EvaluatePrettyPrint(string path)
            => LoadDialog(path)?.WriteTo(Console.Out);

        [MetaCommand("preview", "Creates a somewhat interactive preview of the dialog")]
        private void EvaluatePreview(string path)
        {
            var root = LoadDialog(path);

            if (root != null)
            {
                Preview(root);
                Console.WriteLine("-- End of dialog. --");
            }            
        }

        private void Preview(Dialog dialog)
        {
            Console.WriteLine(dialog.Text);
            Console.WriteLine();

            if (dialog.Responses.Length == 0)
            {
                Console.WriteLine("(No responses)");
            }
            else
            {
                for (var i = 0; i < dialog.Responses.Length; i++)
                {
                    var response = dialog.Responses[i];
                    Console.Write($"{i + 1}. ");
                    Console.WriteLine(response.Text);
                }
            }

            Console.WriteLine("Q. Quit");
            Console.Write("Choose: ");

            var key = Console.ReadLine()?.Trim();

            if (key is null || key.Equals("q", StringComparison.OrdinalIgnoreCase) || !int.TryParse(key, out int index))
            {
                return;
            }

            index--;

            if (index < 0 || index >= dialog.Responses.Length)
            {
                return;
            }

            var selected = dialog.Responses[index];

            switch (selected.Kind)
            {
                case DialogResponseKind.Answer:
                    var t = (AnswerDialogResponse)selected;

                    if (t.Answer is not null)
                    {
                        Console.WriteLine();
                        Console.WriteLine(t.Answer);
                    }
                    break;

                case DialogResponseKind.SubDialog:
                    var sub = ((SubDialogResponse)selected).Dialog;
                    Preview(sub);
                    break;
            }
        }

        [MetaCommand("registry-load", "loads the specified registry into the REPL and makes it accessible though a name")]
        private void EvaluateRegistryLoad(string path, string name)
        {
            var fullPath = Path.Combine(_contextDir, path);

            if (!Directory.Exists(fullPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"error: Directory not found: {path}");
                Console.ResetColor();
                return;
            }

            var registry = DialogRegistry.CreateFromDirectory(fullPath);
            _loadedRegistries.Add(name, registry);
            Console.WriteLine($"Registry \"{name}\" loaded with {registry.Count} dialogs from: {path}");
        }

        [MetaCommand("registry-list", "lists all loaded registries")]
        private void EvaluateRegistryList()
        {
            if (_loadedRegistries.Count == 0)
            {
                Console.WriteLine("No registries loaded.");
                return;
            }

            Console.WriteLine("Loaded registries:");
            foreach (var registry in _loadedRegistries)
            {
                Console.WriteLine($" - {registry.Key} ({registry.Value.Count})");
            }
        }

        [MetaCommand("registry-dump", "pretty prints all of the dialogs in the specified registry")]
        private void EvaluateRegistryDump(string name)
        {
            if (TryFindRegistry(name, out var registry))
            {
                foreach (var dialogEntry in registry)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"-- Dialog: {dialogEntry.Key} --");
                    Console.ResetColor();
                    dialogEntry.Value.WriteTo(Console.Out);
                    Console.WriteLine();
                }
            }
        }

        private Dialog? LoadDialog(string path)
        {
            var fullPath = Path.Combine(_contextDir, path);

            if (!File.Exists(fullPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"error: File not found: {path}");
                Console.ResetColor();
                return null;
            }

            var json = File.ReadAllText(fullPath);
            try
            {
                var dialog = Dialog.FromJson(json);
                return dialog;
            }
            catch (JsonException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"error: Failed to parse JSON: {ex.Message}");
                Console.ResetColor();
                return null;
            }
        }
        private bool TryFindRegistry(string name, [MaybeNullWhen(false)] out DialogRegistry registry)
        {
            var found = _loadedRegistries.TryGetValue(name, out registry);

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"error: Registry not found: {name}");
                Console.ResetColor();
            }
            return found;
        }
    }
}
