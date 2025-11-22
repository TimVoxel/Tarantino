global.registries = ds_map_create()

/// @function load_registry
/// @description creates a new registry, populates it with all of the parsed dialogs inside the specified folder, saves the registry with a name for future access and returns the registry
/// @param path the path to the folder (within datafiles) to load and create a registry upon
/// @param name the name with which to register the registry
function load_dialog_registry(path, name)
{
	function get_filename_without_extension(filename) 
	{
	    var dot_pos = string_last_pos(".", filename);
		
	    if (dot_pos > 0) 
		{
	        return string_copy(filename, 1, dot_pos - 1);
		}
		else 
		{
		    return filename;
		}
	}

	var files = [];
    var fileName = file_find_first("dialogs/*.json", 0);
	
    while (fileName != "")
	{
        array_push(files, fileName);
        fileName = file_find_next();
    }
	
    file_find_close();
	
	var map = ds_map_create();
	
	var fileCount = array_length(files);
	for (var i = 0; i < fileCount; i++)
	{
		fileName = files[i];
		var filePath = path + "/" + fileName;
		var dialog = load_dialog_from_file(filePath);
		var key = get_filename_without_extension(fileName);
		show_debug_message("Loaded " + fileName);
		ds_map_add(map, key, dialog)
	}
	
	ds_map_add(global.registries, name, map);
	return map;
}

/// @function get_dialog_registry(name)
/// @description returns the dialog registry registered with the specified name. If not found, shows an error and returns undefined
/// @param name the name of the registry to search for
function get_dialog_registry(name)
{
	if (ds_map_exists(global.registries, name))
	{
		return ds_map_find_value(global.registries, name);
	}
	
	show_error("The specified dialog registry \"" + name + "\" is not registered", false);
	return undefined;
}


/// @function get_dialog_in_registry
/// @description returns the dialog registered with the specified name in the specified registry registered with the specified name. If either is not found, returns undefined
/// @param registryName the name of the registry in which to search for the dialog
/// @param dialogName the name of the dialog to search for
function get_dialog_in_registry(registryName, dialogName)
{	
	var registry = get_dialog_registry(registryName);
	
	if (registry == undefined)
	{
		return undefined;
	}
	
	if (ds_map_exists(registry, dialogName))
	{
		return ds_map_find_value(registry, dialogName);
	}
	else 
	{
		show_error("The specified dialog \"" + dialogName + "\" is not registered inside the " + registryName + " registry", false);
	}	
}

/// @function get_dialog()
/// @description returns the dialog registered with the specified name in the specified registry. If not found, throws an error and returns undefined 
/// @param registry the registry (object reference) to search
/// @param name the name of the dialog to search for
function get_dialog(registry, name)
{
	if (ds_map_exists(registry, name))
	{
		return ds_map_find_value(registry, name);
	}
	show_error("The specified dialog \"" + name + "\" is not registered inside the provided registry", false);
	return undefined;
}