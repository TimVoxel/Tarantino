/// @function load_dialog_from_file(path)
/// @description Returns a dialog object that was read and parsed from a json file at the specified path 
/// @param path the path to the file to load the dialog from
function load_dialog_from_file(path)
{
	if (!file_exists(path))
	{
		show_error("File not found: " + path + ", cannot load", true);
		return undefined;
	}
	
	var file = file_text_open_read(path);
	var jsonString = "";

    while (!file_text_eof(file)) 
	{
        jsonString += file_text_readln(file);
    }

    file_text_close(file);
    return deserialize_dialog_from_json(jsonString);
}

/// @function deserialize_dialog_from_json(jsonString)
/// @description deserializes the specified json representation of a dialog and returns the result
/// @param jsonString the json representation of tbe dialog
function deserialize_dialog_from_json(jsonString)
{
    var data = json_parse(jsonString);
    return parse_dialog(data);
}

/// @function parse_dialog()
/// @description parses the provided struct and returns a dialog representation
/// @param data the struct to parse
function parse_dialog(data)
{
    var responses = [];
    var count = array_length(data.responses);

	function parse_response(data)
	{
	    var kind = data.kind;

	    switch (kind)
	    {
	        case "Answer":
	            return new AnswerDialogResponse(
	                data.text,
	                (variable_struct_exists(data, "Answer") 
						? data.answer 
						: undefined)
	            );

	        case "SubDialog":
	            return new SubDialogResponse(
	                data.text,
	                parse_dialog(data.dialog)
	            );
			
			default:
				show_error("Unknown DialogResponse kind: " + string(kind), false);
				return undefined;
	    }
	}

    for (var i = 0; i < count; i++)
	{
        responses[i] = parse_response(data.responses[i]);
    }

    return new Dialog(data.text, responses);
}

/// @function pretty_print_dialog(dialog)
/// @description Returns an array of pretty-printed dialog lines.
/// @param dialog the dialog object to pretty print
function pretty_print_dialog(dialog)
{
    var lines = [];

    function write_dialog_inner(l, d, indent)
    {
        if (d == undefined)
		{
			return;
		}

        array_push(l, string_repeat("\t", indent) + "Dialog:");
        indent++;

        var text = variable_struct_exists(d, "text") ? d.text : "(No text)";
        array_push(l, string_repeat("\t", indent) + "\"" + text + "\"");

        var responses = (variable_struct_exists(d, "responses") && d.responses != undefined)
			? d.responses 
			: [];
			
        if (array_length(responses) == 0)
        {
            array_push(l, string_repeat("\t", indent) + "(No responses)");
            return;
        }

        array_push(l, string_repeat("\t", indent) + "Responses:");

        indent++;
        for (var i = 0; i < array_length(responses); i++)
        {
            var response = responses[i];

            if (!variable_struct_exists(response, "kind")) 
			{
                array_push(l, string_repeat("\t", indent) + "(Unknown response type)");
                continue;
            }

            switch (response.kind)
            {
                case "Answer":
			        var resp_text = variable_struct_exists(response, "text") ? response.text : "(No text)";
                    array_push(l, string_repeat("\t", indent) + "- Answer Response: \"" + resp_text + "\"");

                    if (variable_struct_exists(response, "answer") && response.answer != undefined && response.answer != "")
                    {
                        array_push(l, string_repeat("\t", indent) + "  Answer: \"" + response.answer + "\"");
                    }
                    break;

                case "SubDialog":
                    var resp_text2 = variable_struct_exists(response, "text") ? response.text : "(No text)";
                    array_push(l, string_repeat("\t", indent) + "- Sub Dialog: \"" + resp_text2 + "\"");

                    if (variable_struct_exists(response, "dialog"))
					{
                        write_dialog_inner(l, response.dialog, indent + 1);
                    }
                    break;

                default:
					show_error("Unknown response kind: " + response.kind, true);
                    break;
            }
        }
    }

    write_dialog_inner(lines, dialog, 0);
	
	var length = array_length(lines);
	for (var i = 0; i < length; i++)
	{
		show_debug_message(lines[i]);
	}
    return lines;
}