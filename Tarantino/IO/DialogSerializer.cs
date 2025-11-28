using System.Text.Json;

namespace Tarantino.IO
{
    public static class DialogSerializer
    {
        private readonly static DialogJsonConverter _converter = new DialogJsonConverter();
        private readonly static JsonSerializerOptions _defaultOptions = new JsonSerializerOptions
        {
            Converters = { _converter },
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
        };

        public static string Serialize(Dialog dialog)
            => JsonSerializer.Serialize(dialog, _defaultOptions);

        public static string Serialize(Dialog dialog, JsonSerializerOptions options)
        {
            JsonSerializerOptions extendedOptions;

            if (options.Converters.Contains(_converter))
            {
                extendedOptions = options;
            }
            else
            {
                extendedOptions = new JsonSerializerOptions(options);
                extendedOptions.Converters.Add(_converter);
            }
            return JsonSerializer.Serialize(dialog, extendedOptions);
        }

        public static Dialog Deserialize(string json)
        {
            return JsonSerializer.Deserialize<Dialog>(json, _defaultOptions)!;
        }
    }
}
