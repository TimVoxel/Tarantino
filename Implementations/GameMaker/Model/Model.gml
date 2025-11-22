function Dialog(text, responses) constructor {
    self.text = text;
    self.responses = responses;
}

function AnswerDialogResponse(text, answer) constructor {
    self.kind = "Answer";
    self.text = text;
    self.answer = answer;
}

function SubDialogResponse(text, dialog) constructor {
    self.kind = "SubDialog";
    self.text = text;
    self.dialog = dialog;
}