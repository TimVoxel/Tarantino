using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tarantino.IO
{
    public class DialogJsonConverter : JsonConverter<Dialog>
    {
        public override Dialog Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            var text = ImmutableArray.CreateBuilder<TextComponent>();
            var textElement = root.GetProperty("text");

            switch (textElement.ValueKind)
            {
                case JsonValueKind.String:
                    text.Add(new TextComponent(textElement.GetString()!, TextComponentKind.PlainText));
                    break;

                case JsonValueKind.Object:
                    text.Add(ReadTextComponent(textElement));
                    break;

                case JsonValueKind.Array:
                    foreach (var element in textElement.EnumerateArray())
                    {
                        text.Add(ReadTextComponent(element));
                    }
                    break;

                default: 
                    throw new JsonException($"Unexpected kind: {textElement.ValueKind}");
            }
            
            var responses = ImmutableArray.CreateBuilder<DialogResponse>();
            
            if (root.TryGetProperty("responses", out var responseArray))
            {
                foreach (var element in responseArray.EnumerateArray())
                {
                    responses.Add(ReadResponse(element, options));
                }
            }

            

            var events = ReadEvents(root, options);

            return new Dialog(text.ToImmutable(), responses.ToImmutable(), events);
        }

        public override void Write(Utf8JsonWriter writer, Dialog dialog, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (dialog.Text.Length == 1)
            {
                WriteTextComponent(writer, dialog.Text.Single());
            }
            else
            {
                writer.WritePropertyName("text");
                writer.WriteStartArray();
               
                foreach (var element in dialog.Text)
                {
                    WriteTextComponent(writer, element);
                }
                writer.WriteEndArray();
            }
               
            writer.WritePropertyName("responses");
            writer.WriteStartArray();

            foreach (var response in dialog.Responses)
            {
                WriteResponse(writer, response, options);
            }
            
            writer.WriteEndArray();

            writer.WritePropertyName("events");
            writer.WriteStartArray();

            foreach (var response in dialog.Responses)
            {
                WriteResponse(writer, response, options);
            }

            writer.WriteEndArray();

            writer.WriteEndObject();
        }

        private static DialogResponse ReadResponse(JsonElement element, JsonSerializerOptions options)
        {
            var text = element.GetProperty("text").GetString()!;
            var type = Enum.Parse<DialogNodeKind>(element.GetProperty("kind").GetString()!, ignoreCase: true);
            var events = ReadEvents(element, options);

            switch (type)
            {
                case DialogNodeKind.AnswerResponse:
                    var answer = element.TryGetProperty("answer", out var answerElement)
                        ? answerElement.GetString()
                        : null;

                    return new AnswerDialogResponse(text, events, answer);

                case DialogNodeKind.SubDialogResponse:
                    var dialog = JsonSerializer.Deserialize<Dialog>(element.GetProperty("dialog"), options)!;
                    return new SubDialogResponse(text, events, dialog);

                default:
                    throw new JsonException($"Unexpected response type: {type}");
            }
        }
        
        private static void WriteResponse(Utf8JsonWriter writer, DialogResponse response, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("kind", response.Kind.ToString());
            writer.WriteString("text", response.Text);

            switch (response)
            {
                case AnswerDialogResponse textResponse:
                    
                    if (textResponse.Answer != null)
                    {
                        writer.WriteString("answer", textResponse.Answer);
                    }
                    break;

                case SubDialogResponse subResponse:
                    writer.WritePropertyName("dialog");
                    JsonSerializer.Serialize(writer, subResponse.Dialog, options);
                    break;

                default:
                    throw new JsonException($"Unknown DialogResponse type: {response.GetType()}");
            }

            writer.WriteEndObject();
        }

        private static ImmutableArray<DialogEvent> ReadEvents(JsonElement element, JsonSerializerOptions options)
        {
            var events = ImmutableArray.CreateBuilder<DialogEvent>();

            if (element.TryGetProperty("events", out var eventArray))
            {
                foreach (var e in eventArray.EnumerateArray())
                {
                    events.Add(ReadEvent(e, options));
                }
            }

            return events.ToImmutable();
        }

        private static DialogEvent ReadEvent(JsonElement element, JsonSerializerOptions options)
        {
            var kind = Enum.Parse<DialogEventKind>(element.GetProperty("kind").GetString()!);

            switch (kind)
            {
                case DialogEventKind.Tag:
                    var tag = element.GetProperty("tag").GetString()!;
                    return new TagEvent(tag);

                case DialogEventKind.ParameterChange:
                    var parameterName = element.GetProperty("parameter").GetString()!;
                    var newValue = element.GetProperty("value").GetString()!;
                    return new ParameterChangeEvent(parameterName, newValue);

                default:
                    throw new Exception($"Unexected DialogEvent kind: {kind}");
            }
        }

        private static void WriteEvent(Utf8JsonWriter writer, DialogEvent value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("kind", value.Kind.ToString());

            var kind = value.Kind;

            switch (value)
            {
                case TagEvent tagEvent:
                    writer.WriteString("tag", tagEvent.Tag);
                    break;
                case ParameterChangeEvent paramEvent:
                    writer.WriteString("parameter", paramEvent.Parameter);
                    writer.WriteString("value", paramEvent.Value);
                    break;
                default:
                    throw new Exception($"Unexected DialogEvent kind: {kind}");
            }

            writer.WriteEndObject();
        }

        private static TextComponent ReadTextComponent(JsonElement element)
        {
            // String literal = plain text
            if (element.ValueKind == JsonValueKind.String)
            {
                return new TextComponent(element.GetString()!, TextComponentKind.PlainText);
            }

            // Object form
            if (element.ValueKind == JsonValueKind.Object)
            {
                var kind = TextComponentKind.PlainText;

                if (element.TryGetProperty("kind", out var kindProp))
                {
                    kind = Enum.Parse<TextComponentKind>(kindProp.GetString()!, ignoreCase: true);
                }

                var text = element.GetProperty("text").GetString()!;
                return new TextComponent(text, kind);
            }

            throw new JsonException("Invalid TextComponent JSON value.");
        }

        private static void WriteTextComponent(Utf8JsonWriter writer, TextComponent component)
        {
            if (component.Kind == TextComponentKind.PlainText)
            {
                writer.WriteStringValue(component.Text);
                return;
            }

            writer.WriteStartObject();
            writer.WriteString("kind", component.Kind.ToString());
            writer.WriteString("text", component.Text);
            writer.WriteEndObject();
        }
    }
}
