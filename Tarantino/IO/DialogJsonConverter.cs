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
            var text = root.GetProperty("text").GetString()!;
            var responses = ImmutableArray.CreateBuilder<DialogResponse>();

            if (root.TryGetProperty("responses", out var responseArray))
            {
                foreach (var element in responseArray.EnumerateArray())
                {
                    responses.Add(ReadResponse(element, options));
                }
            }

            return new Dialog(text, responses.ToImmutable());
        }

        private static DialogResponse ReadResponse(JsonElement element, JsonSerializerOptions options)
        {
            var text = element.GetProperty("text").GetString()!;
            var type = Enum.Parse<DialogNodeKind>(element.GetProperty("kind").GetString()!, ignoreCase: true);

            switch (type)
            {
                case DialogNodeKind.AnswerResponse:
                    var answer = element.TryGetProperty("answer", out var answerElement)
                        ? answerElement.GetString()
                        : null;

                    return new AnswerDialogResponse(text, answer);

                case DialogNodeKind.SubDialogResponse:
                    var dialog = JsonSerializer.Deserialize<Dialog>(element.GetProperty("dialog"), options)!;
                    return new SubDialogResponse(text, dialog);

                default:
                    throw new JsonException($"Unexpected response type: {type}");
            }
        }

        public override void Write(Utf8JsonWriter writer, Dialog value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("text", value.Text);

            writer.WritePropertyName("responses");
            writer.WriteStartArray();

            foreach (var response in value.Responses)
                WriteResponse(writer, response, options);

            writer.WriteEndArray();
            writer.WriteEndObject();
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
    }
}
