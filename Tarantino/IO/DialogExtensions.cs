using System.CodeDom.Compiler;
using Tarantino.Model;

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

            void WriteColouredIfConsoleLine(ConsoleColor color, string message)
            {
                if (isConsole)
                {
                    Console.ForegroundColor = color;
                    writer.WriteLine(message);
                    Console.ResetColor();
                }
            }

            void WriteColouredIfConsole(ConsoleColor color, string message)
            {
                if (isConsole)
                {
                    Console.ForegroundColor = color;
                    writer.Write(message);
                    Console.ResetColor();
                }
            }

            WriteColouredIfConsole(ConsoleColor.Cyan, $"Dialog");
            WriteColouredIfConsoleLine(ConsoleColor.Gray, ": ");
            WriteColouredIfConsoleLine(ConsoleColor.Gray, $"\"{dialog.Text}\"");
            writer.Indent++;

            if (dialog.Responses.IsDefaultOrEmpty)
            {
                WriteColouredIfConsoleLine(ConsoleColor.DarkGray, "(No responses)");
                writer.Indent--;
                return;
            }

            WriteColouredIfConsole(ConsoleColor.Green, $"Responses");
            WriteColouredIfConsoleLine(ConsoleColor.Gray, ": ");

            writer.Indent++;
            foreach (var response in dialog.Responses)
            {
                switch (response)
                {
                    case AnswerDialogResponse textResponse:
                        WriteColouredIfConsole(ConsoleColor.Gray, "- ");
                        WriteColouredIfConsole(ConsoleColor.Green, $"Answer Response");
                        WriteColouredIfConsole(ConsoleColor.Gray, ": ");
                        WriteColouredIfConsoleLine(ConsoleColor.Gray, $"\"{textResponse.Text}\"");

                        if (textResponse.Answer != null)
                        {
                            WriteColouredIfConsole(ConsoleColor.Green, "  Answer");
                            WriteColouredIfConsole(ConsoleColor.Gray, ": ");
                            WriteColouredIfConsoleLine(ConsoleColor.Gray, $"\"{textResponse.Answer}\"");
                        }
                        break;

                    case SubDialogResponse subResponse:
                        WriteColouredIfConsole(ConsoleColor.Gray, "- ");
                        WriteColouredIfConsole(ConsoleColor.Green, $"Sub dialog");
                        WriteColouredIfConsole(ConsoleColor.Gray, ": ");
                        WriteColouredIfConsoleLine(ConsoleColor.Gray, $"\"{subResponse.Text}\"");
                        writer.Indent++;
                        WriteDialog(subResponse.Dialog, writer);
                        writer.Indent--;
                        break;

                    default:
                        throw new Exception($"Unknown response type {response.GetType()}");
                }
            }
            writer.Indent--;
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
