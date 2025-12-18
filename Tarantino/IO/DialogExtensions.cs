using System.CodeDom.Compiler;
using System.Collections.Immutable;

namespace Tarantino.IO
{
    public static class DialogExtensions
    {
        public static void WriteTo(this Dialog dialog, IndentedTextWriter writer)
        {
            WriteDialog(dialog, writer);
        }

        public static void WriteTo(this Dialog dialog, TextWriter writer)
        {
            WriteDialog(dialog, new IndentedTextWriter(writer));    
        }

        private static void WriteDialog(Dialog dialog, IndentedTextWriter writer)
        {
            var isConsole = IsConsole(writer);

            WriteColouredIfConsole(writer, ConsoleColor.Cyan, $"Dialog", isConsole);
            WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, ": ", isConsole);
            WriteTextComponents(writer, dialog.Text, isConsole);

            writer.Indent++;

            if (dialog.Responses.IsDefaultOrEmpty)
            {
                WriteColouredIfConsoleLine(writer, ConsoleColor.DarkGray, "(No responses)", isConsole);
                writer.Indent--;
                return;
            }

            WriteColouredIfConsole(writer, ConsoleColor.Green, $"Responses", isConsole);
            WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, ": ", isConsole);

            writer.Indent++;
            foreach (var response in dialog.Responses)
            {
                WriteResponse(writer, isConsole, response);
            }

            WriteEvents(writer, dialog.Events, isConsole);

            writer.Indent--;

        }

        private static void WriteResponse(IndentedTextWriter writer, bool isConsole, DialogResponse response)
        {
            switch (response)
            {
                case AnswerDialogResponse textResponse:
                    WriteColouredIfConsole(writer, ConsoleColor.Gray, "- ", isConsole);
                    WriteColouredIfConsole(writer, ConsoleColor.Green, $"Answer Response", isConsole);
                    WriteColouredIfConsole(writer, ConsoleColor.Gray, ": ", isConsole);
                    WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, $"\"{textResponse.Text}\"", isConsole);

                    if (textResponse.Answer != null)
                    {
                        WriteColouredIfConsole(writer, ConsoleColor.Green, "  Answer", isConsole);
                        WriteColouredIfConsole(writer, ConsoleColor.Gray, ": ", isConsole);
                        WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, $"\"{textResponse.Answer}\"", isConsole);
                    }
                    break;

                case SubDialogResponse subResponse:
                    WriteColouredIfConsole(writer, ConsoleColor.Gray, "- ", isConsole);
                    WriteColouredIfConsole(writer, ConsoleColor.Green, $"Sub dialog", isConsole);
                    WriteColouredIfConsole(writer, ConsoleColor.Gray, ": ", isConsole);
                    WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, $"\"{subResponse.Text}\"", isConsole);
                    writer.Indent++;
                    WriteDialog(subResponse.Dialog, writer);
                    writer.Indent--;
                    break;

                case RegistrySubDialogResponse registrySubResponse:
                    WriteColouredIfConsole(writer, ConsoleColor.Gray, "- ", isConsole);
                    WriteColouredIfConsole(writer, ConsoleColor.Green, $"Registry Sub dialog", isConsole);
                    WriteColouredIfConsole(writer, ConsoleColor.Gray, ": ", isConsole);
                    WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, $"\"{registrySubResponse.Text}\"", isConsole);
                    writer.Indent++;
                    WriteColouredIfConsole(writer, ConsoleColor.Green, "  Registry Key", isConsole);
                    WriteColouredIfConsole(writer, ConsoleColor.Gray, ": ", isConsole);
                    WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, $"\"{registrySubResponse.Registry}\"", isConsole);
                    WriteColouredIfConsole(writer, ConsoleColor.Green, "  Dialog Name", isConsole);
                    WriteColouredIfConsole(writer, ConsoleColor.Gray, ": ", isConsole);
                    WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, $"\"{registrySubResponse.DialogName}\"", isConsole);
                    writer.Indent--;
                    break;

                default:
                    throw new Exception($"Unknown response type {response.GetType()}");
            }

            WriteEvents(writer, response.Events, isConsole);
        }

        private static void WriteTextComponents(IndentedTextWriter writer, ImmutableArray<TextComponent> text, bool isConsole)
        {
            if (text.Length == 1)
            {
                var single = text.Single();
                if (single.Kind == TextComponentKind.PlainText)
                {
                    WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, single.Text, isConsole);
                }

                return;
            }

            foreach (var component in text)
            {
                WriteColouredIfConsole(writer, ConsoleColor.Yellow, $"{component.Kind}", isConsole);
                WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, $" - {component.Text}", isConsole);
            }
        }

        private static void WriteEvents(IndentedTextWriter writer, ImmutableArray<DialogEvent> events, bool isConsole)
        {
            if (!events.Any())
            {
                return;
            }

            WriteColouredIfConsole(writer, ConsoleColor.Green, $"  Events", isConsole);
            WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, ": ", isConsole);

            writer.Indent++;

            foreach (var e in events)
            {
                switch (e)
                {
                    case TagEvent tagEvent:
                        WriteColouredIfConsole(writer, ConsoleColor.Gray, "  - ", isConsole);
                        WriteColouredIfConsole(writer, ConsoleColor.Green, $"Tag Event", isConsole);
                        WriteColouredIfConsole(writer, ConsoleColor.Gray, ": ", isConsole);
                        WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, $"\"{tagEvent.Tag}\"", isConsole);
                        break;

                    case ParameterChangeEvent paramChange:
                        WriteColouredIfConsole(writer, ConsoleColor.Gray, "  - ", isConsole);
                        WriteColouredIfConsole(writer, ConsoleColor.Green, $"Parameter Change", isConsole);
                        WriteColouredIfConsole(writer, ConsoleColor.Gray, ": ", isConsole);
                        WriteColouredIfConsole(writer, ConsoleColor.Gray, $"\"{paramChange.Parameter}\" = ", isConsole);
                        WriteColouredIfConsoleLine(writer, ConsoleColor.Yellow, $"\"{paramChange.Value}\"", isConsole);
                        break;

                    default:
                        throw new Exception($"Unknown event type {e.GetType()}");
                }
            }

            writer.Indent--;

        }

        private static void WriteColouredIfConsoleLine(IndentedTextWriter writer, ConsoleColor color, string message, bool isConsole)
        {
            if (isConsole)
            {
                Console.ForegroundColor = color;
                writer.WriteLine(message);
                Console.ResetColor();
            }
        }

        private static void WriteColouredIfConsole(IndentedTextWriter writer, ConsoleColor color, string message, bool isConsole)
        {
            if (isConsole)
            {
                Console.ForegroundColor = color;
                writer.Write(message);
                Console.ResetColor();
            }
        }

        private static bool IsConsole(TextWriter writer)
        {
            if (writer == Console.Out)
                return true;

            if (writer == Console.Error)
                return !Console.IsErrorRedirected && !Console.IsOutputRedirected;

            return writer is IndentedTextWriter iw && IsConsole(iw.InnerWriter);
        }
    }
}
