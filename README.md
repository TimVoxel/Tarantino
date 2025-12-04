# Tarantino

Tarantino is a dialog format that represents "Dragon Age: Origins" style dialogs.<br>
This repo contains both the model library and a REPL and WFI for managing the dialogs.

## The format
Every tarantino dialog is a tree of dialog nodes. You always start with a dialog. 

A dialog node has a text property and a list of responses. When the dialog is previewed, the text of the dialog node is shown to the player. They are presented with the array of responses and are free to choose which one they like. Each response also has a text field that is displayed to the player.<br>

There are 2 types of responses:<br>
  1. Answer responses. These are the simplest reponse you can have, containing only the text and an answer. If the player selects this option, the NPS will answer with the "Answer" property. If the answer property is not set, the dialog will end.<br>
  2. Sub dialog. These contain a sub dialog node in them. When a sub dialog option is selected, it's sub dialog is shown to the player. This continues until the player selects an answer response (or if the sub dialog has no responses, which is undesirable).

## Events
Every dialog node can have an optional list of events. Events allow you to add additional behaviour to the dialog. For instance, by adding an event to a response, you can handle the event to, for instance, change the background.<br>

There are 2 types of events:
  1. Tag events. They can be seen as flags. Their only property is the tag, therefore you can add different behaviour for different tags.
  2. Parameter change events. They allow you to pass a value and the name of the parameter that should change. This can be used to change context variables and state that requires additional data. WARNING: at the moment every parameter value is always a string.
