using System;
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
            WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, $"\"{dialog.Text}\"", isConsole);
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
                        WriteColouredIfConsole(writer,ConsoleColor.Green, $"Sub dialog", isConsole);
                        WriteColouredIfConsole(writer, ConsoleColor.Gray, ": ", isConsole);
                        WriteColouredIfConsoleLine(writer, ConsoleColor.Gray, $"\"{subResponse.Text}\"", isConsole);
                        writer.Indent++;
                        WriteDialog(subResponse.Dialog, writer);
                        writer.Indent--;
                        break;

                    default:
                        throw new Exception($"Unknown response type {response.GetType()}");
                }

                WriteEvents(writer, response.Events, isConsole);
            }

            WriteEvents(writer, dialog.Events, isConsole);

            writer.Indent--;

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
